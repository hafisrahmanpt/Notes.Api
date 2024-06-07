using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes.Api.data;
using Notes.Api.Model.Entities;

namespace Notes.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        private readonly NotesDbContext notesDbContext;

        public NotesController(NotesDbContext notesDbContext)
        {
            this.notesDbContext = notesDbContext;
        }

        [HttpGet]
        public async Task<IActionResult>GetAllNotes()
        {
            //Get Notes from the datebase.
            return Ok(await notesDbContext.Notes.ToListAsync());
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetNoteById")]
        public async Task<IActionResult>GetNoteById([FromRoute]Guid id)
        {
           var note= await notesDbContext.Notes.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }
            return Ok(note);
        }

        [HttpPost]
        public async Task<IActionResult>AddNotes(Note note)
        {
            note.Id=Guid.NewGuid();
            await notesDbContext.Notes.AddAsync(note);
            await notesDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetNoteById), new {id = note.Id, note});
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult>UpdateNote([FromRoute]Guid id,[FromBody]Note updatednote)
        {
            var existingnote = await notesDbContext.Notes.FindAsync(id);
             if (existingnote == null)
             {
                return NotFound();
             }
                
             existingnote.Title= updatednote.Title;
             existingnote.Description = updatednote.Description;
             existingnote.IsVisible = updatednote.IsVisible;
            await notesDbContext.SaveChangesAsync();
            return Ok(existingnote);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteNote([FromRoute] Guid id, [FromBody] Note updatednote)
        {
            var existingnote = await notesDbContext.Notes.FindAsync(id);
            if (existingnote == null)
            {
                return NotFound();
            }

            notesDbContext.Notes.Remove(existingnote);
            await notesDbContext.SaveChangesAsync();
            return Ok();
   
        }

    }
}
