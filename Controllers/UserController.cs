using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers;

public class UserController : Controller
{
    public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();

    // GET: User
    public ActionResult Index(string? searchTerm = null)
    {
        ViewData["CurrentFilter"] = searchTerm;
        IEnumerable<User> users = userlist;

        if (!string.IsNullOrEmpty(searchTerm))
        {
            searchTerm = searchTerm.ToLower();
            users = userlist.Where(u =>
                (u.Name?.ToLower().Contains(searchTerm) ?? false) ||
                (u.Email?.ToLower().Contains(searchTerm) ?? false) ||
                u.Id.ToString().Contains(searchTerm)
            );
        }

        return View(users.ToList());
    }

    // GET: User/Details/5
    public ActionResult Details(int id)
    {
        // Find the user by id
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // GET: User/Create
    public ActionResult Create()
    {
        // Show the Create view
        return View();
    }

    // POST: User/Create
    [HttpPost]
    public ActionResult Create(User user)
    {
        if (ModelState.IsValid)
        {
            userlist.Add(user);
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    // GET: User/Edit/5
    public ActionResult Edit(int id)
    {
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // POST: User/Edit/5
    [HttpPost]
    public ActionResult Edit(int id, User user)
    {
        var existingUser = userlist.FirstOrDefault(u => u.Id == id);
        if (existingUser == null)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    // GET: User/Delete/5
    public ActionResult Delete(int id)
    {
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // POST: User/Delete/5
    [HttpPost]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        userlist.Remove(user);
        return RedirectToAction(nameof(Index));
    }
}