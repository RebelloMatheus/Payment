using Microsoft.EntityFrameworkCore;

namespace Payment.Infra.DataBase.Context.Base
{
    public abstract class ContextBase : DbContext, IContextBase
    {
        public abstract string ContextName { get; }

        protected ContextBase(DbContextOptions options) : base(options)
        { }
    }
}