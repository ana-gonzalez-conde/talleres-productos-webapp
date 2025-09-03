using Model.Daos.BankCardDao;
using Model.Daos.OrderDao;
using Model.Daos.OrderLineDao;
using Model.Daos.CategoryDao;
using Model.Daos.ProductDao;
using Model.Daos.UserDao;
using Model.Daos.CommentDao;
using Model.Daos.TagDao;
using Model.Services;
using Model.Services.ProductService;
using Model.Services.UserService;
using Model.Services.OrderService;
using Model.Services.CommentService;
using Ninject;
using System.Configuration;
using System.Data.Entity;

namespace Test
{
    class TestManager
    {
        /// <summary>
        /// Configures and populates the Ninject kernel
        /// </summary>
        /// <returns>The NInject kernel</returns>
        public static IKernel ConfigureNInjectKernel()
        {
            NinjectSettings settings = new NinjectSettings() { LoadExtensions = true };

            IKernel kernel = new StandardKernel(settings);

            kernel.Bind<IUserDao>().
                To<UserDaoEntityFramework>();

            kernel.Bind<IBankCardDao>().
                To<BankCardDaoEntityFramework>();

            kernel.Bind<IOrderDao>().
                To<OrderDaoEntityFramework>();

            kernel.Bind<IOrderLineDao>().
                To<OrderLineDaoEntityFramework>();

            kernel.Bind<ICategoryDao>().
                To<CategoryDaoEntityFramework>();

            kernel.Bind<IProductDao>().
                To<ProductDaoEntityFramework>();

            kernel.Bind<ICommentDao>().
                To<CommentDaoEntityFramework>();

            kernel.Bind<ITagDao>().
                To<TagDaoEntityFramework>();

            kernel.Bind<IUserService>().
                To<UserService>();

            kernel.Bind<IOrderService>().
                To<OrderService>();

            kernel.Bind<IProductService>().
                To<ProductService>();

            kernel.Bind<ICommentService>().
                To<CommentService>();

            string connectionString =
                ConfigurationManager.ConnectionStrings["practicamadEntities"].ConnectionString;

            kernel.Bind<DbContext>().
                ToSelf().
                InSingletonScope().
                WithConstructorArgument("nameOrConnectionString", connectionString);

            return kernel;
        }

        /// <summary>
        /// Configures the Ninject kernel from an external module file.
        /// </summary>
        /// <param name="moduleFilename">The module filename.</param>
        /// <returns>The NInject kernel</returns>
        public static IKernel ConfigureNInjectKernel(string moduleFilename)
        {
            NinjectSettings settings = new NinjectSettings() { LoadExtensions = true };
            IKernel kernel = new StandardKernel(settings);

            kernel.Load(moduleFilename);

            return kernel;
        }

        public static void ClearNInjectKernel(IKernel kernel)
        {
            kernel.Dispose();
        }
    }
}
