using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAPI;

namespace TestAPI.controller {

    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase {

        private readonly TestContext _context;

        public TestController(TestContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestItem>>> GettestItems() {
            return await _context.testItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TestItem>> GetTestItem(int id) {
            var testItem = await _context.testItems.FindAsync(id);
            if (testItem == null) {
                return NotFound();
            }
            return testItem;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTestItem(int id, TestItem testItem) {
            if (id != testItem.Id) {
                return BadRequest();
            }

            _context.Entry(testItem).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (Microsoft.EntityFrameworkCore.DbUpdateException) {
                return Conflict();
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TestItem>> PostTestItem(TestItem testItem) {
            _context.testItems.Add(testItem);
            try {
                await _context.SaveChangesAsync();
            } catch (Microsoft.EntityFrameworkCore.DbUpdateException) {
                return Conflict();
            }

            return CreatedAtAction("GetTestItem", new { id = testItem.Id }, testItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestItem(int id) {
            var testItem = await _context.testItems.FindAsync(id);
            if (testItem == null) {
                return NotFound();
            }

            _context.testItems.Remove(testItem);

            try {
                await _context.SaveChangesAsync();
            } catch (Microsoft.EntityFrameworkCore.DbUpdateException) {
                return Conflict();
            }

            return NoContent();
        }

        private bool TestItemExists(int id) {
            return _context.testItems.Any(e => e.Id == id);
        }
    }
}
