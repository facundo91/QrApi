namespace qrAPI.DAL.Tests
{
    using System;
    using System.Threading.Tasks;
    using Data.EFData.Contexts;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// <see href="https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/sqlite">Microsoft Documentation.</see>
    /// Represents an in-memory SQLite database used for testing purposes.
    /// The associated database is deleted from memory when the associated
    /// <see cref="TestDatabase"/> object is disposed.
    /// This class cannot be inherited.
    /// <see href="https://github.com/dotnet/EntityFramework.Docs/issues/2223#issue-588422607">Issue about this solution.</see>
    /// </summary>
    /// <remarks>
    /// Migrations are not used, instead the current model state is applied to the
    /// SQLite database because not all migration features are supported by SQLite.
    /// For more information about SQLite limiations, please refer to
    /// https://docs.microsoft.com/en-us/ef/core/providers/sqlite/limitations.
    /// </remarks>
    /// <remarks>
    /// Because some testing frameworks might not (easisly) support asynchronous operations,
    /// public members of this class are implemented both synchronously and asynchronously.
    /// It is preferred to use the asynchronous operations wherever possible.
    /// </remarks>
    public sealed class TestDatabase : IDisposable, IAsyncDisposable
    {
        private static readonly string InMemoryConnectionString = "DataSource=:memory:";
        private SqliteConnection? _connection;

        /// <summary>
        /// Synchronously checks if the database needs to be created,
        /// asynchronously creates the database if required
        /// and synchronously returns a new <see cref="ApplicationDbContext"/>.
        /// </summary>
        /// <returns>The created <see cref="ApplicationDbContext"/>.</returns>
        /// <remarks>
        /// This method only executes asynchronously the first time it is called
        /// per instance of <see cref="TestDatabase"/>.
        /// </remarks>
        public async Task<ApplicationDbContext> CreateContextAsync()
        {
            await SetupIfNecessaryAsync().ConfigureAwait(false);
            return CreateContextPrivate();
        }

        /// <summary>
        /// Synchronously check if the database needs to be created,
        /// creates the database if required
        /// and returns a new <see cref="ApplicationDbContext"/>.
        /// </summary>
        /// <returns>The created <see cref="ApplicationDbContext"/>.</returns>
        public ApplicationDbContext CreateContext()
        {
            SetupIfNecessary();
            return CreateContextPrivate();
        }

        /// <inheritdoc />
        /// <remarks>
        /// Disposing this instance destroys the related in-memory database.
        /// </remarks>
        public ValueTask DisposeAsync()
            => _connection?.DisposeAsync() ?? new ValueTask(Task.CompletedTask);

        /// <inheritdoc />
        /// <remarks>
        /// Disposing this instance destroys the related in-memory database.
        /// </remarks>
        public void Dispose()
            => _connection?.Dispose();

        private async Task SetupIfNecessaryAsync()
        {
            if (_connection is null)
            {
                PrepareConnection();
                await _connection!.OpenAsync().ConfigureAwait(false);
                using var ctx = CreateContextPrivate();
                await ctx.Database.EnsureCreatedAsync().ConfigureAwait(false);
            }
        }

        private void SetupIfNecessary()
        {
            if (_connection is null)
            {
                PrepareConnection();
                _connection!.OpenAsync();
                using var ctx = CreateContextPrivate();
                ctx.Database.EnsureCreated();
            }
        }

        private void PrepareConnection() =>
            _connection = new SqliteConnection(InMemoryConnectionString);

        private ApplicationDbContext CreateContextPrivate() =>
            new ApplicationDbContext(
                new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlite(
                        _connection ?? throw new InvalidOperationException(
                            $"Instance variable \'{_connection}\' has not been initialized."))
                        .Options);
    }
}
