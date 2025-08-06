using MeuProjeto.Data;
using MeuProjeto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeuProjeto.Controllers;

public class TarefasController : Controller
{
    private readonly AppDbContext _context;

    public TarefasController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Tarefas
    public async Task<IActionResult> Index()
    {
        return View(await _context.Tarefas.ToListAsync());
    }

    // GET: Tarefas/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var tarefa = await _context.Tarefas.FirstOrDefaultAsync(m => m.Id == id);
        if (tarefa == null) return NotFound();
        return View(tarefa);
    }

    // GET: Tarefas/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Tarefas/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Descricao,Concluida")] Tarefa tarefa)
    {
        if (ModelState.IsValid)
        {
            _context.Add(tarefa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(tarefa);
    }

    // GET: Tarefas/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var tarefa = await _context.Tarefas.FindAsync(id);
        if (tarefa == null) return NotFound();
        return View(tarefa);
    }

    // POST: Tarefas/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao,Concluida")] Tarefa tarefa)
    {
        if (id != tarefa.Id) return NotFound();
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(tarefa);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Tarefas.Any(e => e.Id == tarefa.Id))
                    return NotFound();
                else
                    throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(tarefa);
    }

    // GET: Tarefas/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var tarefa = await _context.Tarefas.FirstOrDefaultAsync(m => m.Id == id);
        if (tarefa == null) return NotFound();
        return View(tarefa);
    }

    // POST: Tarefas/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);
        if (tarefa != null)
        {
            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}