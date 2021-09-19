using Microsoft.EntityFrameworkCore;

namespace TestAPI {

    public class TestContext : DbContext {

        public TestContext(DbContextOptions<TestContext> options) : base (options) {
        }

        public DbSet<TestItem> testItems { get; set; }
    }
}