using ControlProduccion.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControlProduccion.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagementController(UserManager<IdentityUser> userManager,
                                        RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        //create a view to get all users and their roles
        [HttpGet]
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            

            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleLockout(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Si el usuario está bloqueado (LockoutEnd mayor a la fecha actual) se procede a desbloquearlo
            if (user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTimeOffset.UtcNow)
            {
                user.LockoutEnd = DateTimeOffset.UtcNow; // Lo desbloquea
            }
            else
            {
                // Bloquea al usuario estableciendo un LockoutEnd en el futuro (por ejemplo, 100 años)
                user.LockoutEnd = DateTimeOffset.UtcNow.AddYears(100);
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                // Maneja los errores según lo necesites (por ejemplo, enviándolos a la vista)
                ModelState.AddModelError("", "No se pudo actualizar el estado del usuario.");
            }

            return RedirectToAction("Index");
        }



        // GET: Muestra la pantalla de creación de usuario
        [HttpGet]
      
        public IActionResult Create()
        {
            var model = new CreateUserViewModel
            {
                // Cargar la lista de roles disponibles desde el RoleManager
                Roles = _roleManager.Roles.Select(r => new SelectListItem
                {
                    Value = r.Name,
                    Text = r.Name
                })
            };

            return View(model);
        }

        // POST: Recibe el formulario de creación de usuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Crear el usuario
                var user = new IdentityUser
                {
                    UserName = model.UserName,
                    Email = model.UserName
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Asignar el rol seleccionado al usuario
                    var roleResult = await _userManager.AddToRoleAsync(user, model.SelectedRole);
                    if (roleResult.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        // Si falla la asignación del rol, agregar errores
                        foreach (var error in roleResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    // Agregar errores de creación del usuario
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            // Recargar la lista de roles en caso de error y mostrar la vista de nuevo
            model.Roles = _roleManager.Roles.Select(r => new SelectListItem
            {
                Value = r.Name,
                Text = r.Name
            });

            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Obtener el rol actual del usuario (se asume que el usuario tiene un único rol)
            var roles = await _userManager.GetRolesAsync(user);
            var currentRole = roles.FirstOrDefault();

            var model = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                SelectedRole = currentRole,
                Roles = _roleManager.Roles.Select(r => new SelectListItem
                {
                    Value = r.Name,
                    Text = r.Name
                })
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Recargar la lista de roles en caso de error
                model.Roles = _roleManager.Roles.Select(r => new SelectListItem
                {
                    Value = r.Name,
                    Text = r.Name
                });
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return NotFound();
            }

            // Actualizar el username
            user.UserName = model.UserName;
            user.NormalizedUserName = _userManager.NormalizeName(model.UserName);

            // Si se ingresa un nuevo password, se actualiza
            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                // Generar un token para resetear la contraseña
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResult = await _userManager.ResetPasswordAsync(user, token, model.Password);
                if (!passwordResult.Succeeded)
                {
                    foreach (var error in passwordResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    // Recargar la lista de roles
                    model.Roles = _roleManager.Roles.Select(r => new SelectListItem
                    {
                        Value = r.Name,
                        Text = r.Name
                    });
                    return View(model);
                }
            }

            // Actualizar los datos básicos del usuario
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                // Recargar la lista de roles
                model.Roles = _roleManager.Roles.Select(r => new SelectListItem
                {
                    Value = r.Name,
                    Text = r.Name
                });
                return View(model);
            }

            // Actualizar el rol si es diferente
            var currentRoles = await _userManager.GetRolesAsync(user);
            if (!currentRoles.Contains(model.SelectedRole))
            {
                // Remover todos los roles actuales
                var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeResult.Succeeded)
                {
                    foreach (var error in removeResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    model.Roles = _roleManager.Roles.Select(r => new SelectListItem
                    {
                        Value = r.Name,
                        Text = r.Name
                    });
                    return View(model);
                }

                // Agregar el nuevo rol
                var addRoleResult = await _userManager.AddToRoleAsync(user, model.SelectedRole);
                if (!addRoleResult.Succeeded)
                {
                    foreach (var error in addRoleResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    model.Roles = _roleManager.Roles.Select(r => new SelectListItem
                    {
                        Value = r.Name,
                        Text = r.Name
                    });
                    return View(model);
                }
            }

            return RedirectToAction("Index");
        }

    }
}
