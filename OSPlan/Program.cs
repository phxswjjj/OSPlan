using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;

namespace OSPlan
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer();
            container.RegisterType<IRepository<ProductPartEntity>, JsonContext>(new Unity.Lifetime.PerResolveLifetimeManager(), new InjectionConstructor("data.json"));
            //container.RegisterType<IRepository<ProductPartEntity>, MockContext>(new Unity.Lifetime.PerResolveLifetimeManager());
            //SaveRepository(container);

            var products = container.Resolve<ProductRepository>().ReadAll();
            var s = JsonConvert.SerializeObject(products, Formatting.Indented);
            Console.WriteLine(s);
            Console.ReadLine();
        }

        static void SaveRepository(IUnityContainer container)
        {
            var repo = container.Resolve<IRepository<ProductPartEntity>>();
            var data = repo.ReadAll();

            var s = JsonConvert.SerializeObject(data, Formatting.Indented);
            using (var writter = new StreamWriter("tmp.json"))
            {
                writter.Write(s);
            }
        }
    }

    class JsonContext : IRepository<ProductPartEntity>
    {
        List<ProductPartEntity> Content;
        public JsonContext(string path)
        {
            using (var reader = new StreamReader(path))
            {
                var s = reader.ReadToEnd();
                this.Content = JsonConvert.DeserializeObject<List<ProductPartEntity>>(s);
            }
        }

        public ProductPartEntity Read(Predicate<ProductPartEntity> find)
        {
            return this.Content.Find(find);
        }

        public IEnumerable<ProductPartEntity> ReadAll()
        {
            return this.Content;
        }

        public IEnumerable<ProductPartEntity> Reads(Predicate<ProductPartEntity> find)
        {
            return this.Content.FindAll(find);
        }
    }

    class MockContext : IRepository<ProductPartEntity>
    {
        List<ProductPartEntity> Content;
        public MockContext()
        {
            var content = new List<ProductPartEntity>();
            content.Add(new ProductPartEntity() { ProductName = "P1", PartType = PartType.ProbeCard, PartName = "PC1", Avaiable = 5 });
            content.Add(new ProductPartEntity() { ProductName = "P1", PartType = PartType.ProbeCard, PartName = "PC2", Avaiable = 2 });
            content.Add(new ProductPartEntity() { ProductName = "P1", PartType = PartType.ProbeCard, PartName = "PC3", Avaiable = 1 });
            content.Add(new ProductPartEntity() { ProductName = "P1", PartType = PartType.Sensor, PartName = "S1", Avaiable = 1 });
            content.Add(new ProductPartEntity() { ProductName = "P1", PartType = PartType.Sensor, PartName = "S2", Avaiable = 2 });
            content.Add(new ProductPartEntity() { ProductName = "P1", PartType = PartType.Sensor, PartName = "S3", Avaiable = 3 });

            //content.Add(new ProductPartEntity() { ProductName = "P2", PartType = PartType.ProbeCard, PartName = "PC1", Avaiable = 5 });
            content.Add(new ProductPartEntity() { ProductName = "P2", PartType = PartType.ProbeCard, PartName = "PC2", Avaiable = 2 });
            content.Add(new ProductPartEntity() { ProductName = "P2", PartType = PartType.ProbeCard, PartName = "PC3", Avaiable = 1 });
            content.Add(new ProductPartEntity() { ProductName = "P2", PartType = PartType.Sensor, PartName = "S1", Avaiable = 1 });
            //content.Add(new ProductPartEntity() { ProductName = "P2", PartType = PartType.Sensor, PartName = "S2", Avaiable = 2 });
            //content.Add(new ProductPartEntity() { ProductName = "P2", PartType = PartType.Sensor, PartName = "S3", Avaiable = 3 });

            //content.Add(new ProductPartEntity() { ProductName = "P3", PartType = PartType.ProbeCard, PartName = "PC1", Avaiable = 5 });
            content.Add(new ProductPartEntity() { ProductName = "P3", PartType = PartType.ProbeCard, PartName = "PC2", Avaiable = 2 });
            content.Add(new ProductPartEntity() { ProductName = "P3", PartType = PartType.ProbeCard, PartName = "PC3", Avaiable = 1 });
            //content.Add(new ProductPartEntity() { ProductName = "P3", PartType = PartType.Sensor, PartName = "S1", Avaiable = 1 });
            content.Add(new ProductPartEntity() { ProductName = "P3", PartType = PartType.Sensor, PartName = "S2", Avaiable = 2 });
            //content.Add(new ProductPartEntity() { ProductName = "P3", PartType = PartType.Sensor, PartName = "S3", Avaiable = 3 });

            content.Add(new ProductPartEntity() { ProductName = "P4", PartType = PartType.ProbeCard, PartName = "PC1", Avaiable = 5 });
            //content.Add(new ProductPartEntity() { ProductName = "P4", PartType = PartType.ProbeCard, PartName = "PC2", Avaiable = 2 });
            content.Add(new ProductPartEntity() { ProductName = "P4", PartType = PartType.ProbeCard, PartName = "PC3", Avaiable = 1 });
            content.Add(new ProductPartEntity() { ProductName = "P4", PartType = PartType.Sensor, PartName = "S1", Avaiable = 1 });
            //content.Add(new ProductPartEntity() { ProductName = "P4", PartType = PartType.Sensor, PartName = "S2", Avaiable = 2 });
            content.Add(new ProductPartEntity() { ProductName = "P4", PartType = PartType.Sensor, PartName = "S3", Avaiable = 3 });

            content.Add(new ProductPartEntity() { ProductName = "P5", PartType = PartType.ProbeCard, PartName = "PC1", Avaiable = 5 });
            content.Add(new ProductPartEntity() { ProductName = "P5", PartType = PartType.ProbeCard, PartName = "PC2", Avaiable = 2 });
            content.Add(new ProductPartEntity() { ProductName = "P5", PartType = PartType.ProbeCard, PartName = "PC3", Avaiable = 1 });
            content.Add(new ProductPartEntity() { ProductName = "P5", PartType = PartType.Sensor, PartName = "S1", Avaiable = 1 });
            content.Add(new ProductPartEntity() { ProductName = "P5", PartType = PartType.Sensor, PartName = "S2", Avaiable = 2 });
            content.Add(new ProductPartEntity() { ProductName = "P5", PartType = PartType.Sensor, PartName = "S3", Avaiable = 3 });

            content.Add(new ProductPartEntity() { ProductName = "P6", PartType = PartType.ProbeCard, PartName = "PC1", Avaiable = 5 });
            //content.Add(new ProductPartEntity() { ProductName = "P6", PartType = PartType.ProbeCard, PartName = "PC2", Avaiable = 2 });
            //content.Add(new ProductPartEntity() { ProductName = "P6", PartType = PartType.ProbeCard, PartName = "PC3", Avaiable = 1 });
            //content.Add(new ProductPartEntity() { ProductName = "P6", PartType = PartType.Sensor, PartName = "S1", Avaiable = 1 });
            content.Add(new ProductPartEntity() { ProductName = "P6", PartType = PartType.Sensor, PartName = "S2", Avaiable = 2 });
            content.Add(new ProductPartEntity() { ProductName = "P6", PartType = PartType.Sensor, PartName = "S3", Avaiable = 3 });
            this.Content = content;
        }

        public ProductPartEntity Read(Predicate<ProductPartEntity> find)
        {
            return this.Content.Find(find);
        }

        public IEnumerable<ProductPartEntity> ReadAll()
        {
            return this.Content;
        }

        public IEnumerable<ProductPartEntity> Reads(Predicate<ProductPartEntity> find)
        {
            return this.Content.FindAll(find);
        }
    }
}
