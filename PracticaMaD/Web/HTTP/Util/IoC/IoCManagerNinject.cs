using Es.Udc.DotNet.ModelUtil.IoC;
using Model.Daos.CategoryDao;
using Model.Daos.ProductDao;
using Model.Daos.UserDao;
using Model.Services.ProductService;
using Model.Daos.BankCardDao;
using Model.Services;
using Model.Services.UserService;
using Ninject;
using System.Configuration;
using System.Data.Entity;
using Model.Services.OrderService;
using Model.Daos.OrderDao;
using Model.Daos.OrderLineDao;
using Model.Services.CommentService;
using Model.Daos.CommentDao;
using Model.Daos.TagDao;

namespace Es.Udc.DotNet.PracticaMaD.HTTP.Util.IoC
{
    internal class IoCManagerNinject : IIoCManager
    {
        private static IKernel kernel;
        private static NinjectSettings settings;

        public void Configure()
        {
            settings = new NinjectSettings() { LoadExtensions = true };
            kernel = new StandardKernel(settings);

            /*** ProductService ***/
            kernel.Bind<IProductService>().To<ProductService>();

            /*** CommentService ***/
            kernel.Bind<ICommentService>().To<CommentService>();

            /*** OrderService ***/
            kernel.Bind<IOrderService>().To<OrderService>();

            /*** CommentDao ***/
            kernel.Bind<ICommentDao>().
                To<CommentDaoEntityFramework>();

            /*** TagDao ***/
            kernel.Bind<ITagDao>().
                To<TagDaoEntityFramework>();

            /*** UserDao ***/
            kernel.Bind<IUserDao>().
                To<UserDaoEntityFramework>();

            /*** ProductDao ***/
            kernel.Bind<IProductDao>().
                To<ProductDaoEntityFramework>();

            /*** CategoryDao ***/
            kernel.Bind<ICategoryDao>().
                To<CategoryDaoEntityFramework>();
                
            /* UserService */
            kernel.Bind<IUserService>().
                To<UserService>();

            /* BankCardDao */
            kernel.Bind<IBankCardDao>().
                To<BankCardDaoEntityFramework>();

            /* OrderDao */
            kernel.Bind<IOrderDao>().
                To<OrderDaoEntityFramework>();

            /* OrderLineDao */
            kernel.Bind<IOrderLineDao>().
                To<OrderLineDaoEntityFramework>();

            /*** DbContext ***/
            string connectionString =
                ConfigurationManager.ConnectionStrings["practicamadEntities"].ConnectionString;

            kernel.Bind<DbContext>().
                    ToSelf().
                    InSingletonScope().
                    WithConstructorArgument("nameOrConnectionString", connectionString);
        }

        public T Resolve<T>()
        {
            return kernel.Get<T>();
        }
    }
}