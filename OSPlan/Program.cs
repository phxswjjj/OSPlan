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
            var isMock = true;
            var container = new UnityContainer();
            if (isMock)
            {
                container.RegisterType<IRepository<ProductPartRelation>, MockProductPart>(new Unity.Lifetime.PerResolveLifetimeManager());
                container.RegisterType<IRepository<Part>, MockPart>(new Unity.Lifetime.PerResolveLifetimeManager());
                container.RegisterType<IRepository<ProductEqpPlan>, MockMCPlan>(new Unity.Lifetime.PerResolveLifetimeManager());
                SaveRepository(container);
            }
            else
            {
                container.RegisterType<IRepository<ProductPartRelation>, JsonContext<ProductPartRelation>>(new Unity.Lifetime.PerResolveLifetimeManager(), new InjectionConstructor("data.json"));
                container.RegisterType<IRepository<Part>, JsonContext<Part>>(new Unity.Lifetime.PerResolveLifetimeManager(), new InjectionConstructor("parts.json"));
                container.RegisterType<IRepository<ProductEqpPlan>, JsonContext<ProductEqpPlan>>(new Unity.Lifetime.PerResolveLifetimeManager(), new InjectionConstructor("plans.json"));
            }

            var repo = container.Resolve<ProductRepository>();
            var products = repo.ReadAll();

            #region sort products..

            #endregion

            var avaiableCount = 10;
            var totalPlanCount = 0;
            foreach (var product in products)
            {
                var planCount = product.ApplyPlan(avaiableCount);
                totalPlanCount += planCount;
                avaiableCount -= planCount;
                if (avaiableCount <= 0) break;
            }

            var results = from product in products
                          select new { Product = product.Name, Eqp = product.EqpCount };
            var s = JsonConvert.SerializeObject(results, Formatting.Indented);
            using (var writter = new StreamWriter("result.json"))
            {
                writter.Write(s);
            }
            Console.WriteLine(s);

            //check parts result
            var parts = repo.PartRepo.ReadAll();
            var s2 = JsonConvert.SerializeObject(parts, Formatting.Indented);
            using (var writter = new StreamWriter("result-part.json"))
            {
                writter.Write(s2);
            }

            Console.WriteLine($"總開機數：{totalPlanCount}, 閒置機台數：{avaiableCount}");

            Console.WriteLine();
            Console.WriteLine("press ENTER to be continue...");
            Console.ReadLine();
        }

        static void SaveRepository(IUnityContainer container)
        {
            var productRepo = container.Resolve<IRepository<ProductPartRelation>>();
            var productData = productRepo.ReadAll();

            var productResult = JsonConvert.SerializeObject(productData, Formatting.Indented);
            using (var writter = new StreamWriter("data.json"))
            {
                writter.Write(productResult);
            }

            var partRepo = container.Resolve<IRepository<Part>>();
            var partData = partRepo.ReadAll();

            var partResult = JsonConvert.SerializeObject(partData, Formatting.Indented);
            using (var writter = new StreamWriter("parts.json"))
            {
                writter.Write(partResult);
            }

            var planRepo = container.Resolve<IRepository<ProductEqpPlan>>();
            var planData = planRepo.ReadAll();

            var planResult = JsonConvert.SerializeObject(planData, Formatting.Indented);
            using (var writter = new StreamWriter("plans.json"))
            {
                writter.Write(planResult);
            }
        }
    }

    class JsonContext<T> : IRepository<T>
    {
        List<T> Content;
        public JsonContext(string path)
        {
            using (var reader = new StreamReader(path))
            {
                var s = reader.ReadToEnd();
                this.Content = JsonConvert.DeserializeObject<List<T>>(s);
            }
        }

        public T Read(Predicate<T> find)
        {
            return this.Content.Find(find);
        }

        public IEnumerable<T> ReadAll()
        {
            return this.Content;
        }

        public IEnumerable<T> Reads(Predicate<T> find)
        {
            return this.Content.FindAll(find);
        }
    }

    class MockProductPart : IRepository<ProductPartRelation>
    {
        List<ProductPartRelation> Content;
        public MockProductPart()
        {
            var content = new List<ProductPartRelation>();
            content.Add(new ProductPartRelation() { ProductName = "P1", PartType = PartType.ProbeCard, PartName = "PC1" });
            content.Add(new ProductPartRelation() { ProductName = "P1", PartType = PartType.ProbeCard, PartName = "PC2" });
            content.Add(new ProductPartRelation() { ProductName = "P1", PartType = PartType.ProbeCard, PartName = "PC3" });
            content.Add(new ProductPartRelation() { ProductName = "P1", PartType = PartType.Sensor, PartName = "S1" });
            content.Add(new ProductPartRelation() { ProductName = "P1", PartType = PartType.Sensor, PartName = "S2" });
            content.Add(new ProductPartRelation() { ProductName = "P1", PartType = PartType.Sensor, PartName = "S3" });

            content.Add(new ProductPartRelation() { ProductName = "P2", PartType = PartType.ProbeCard, PartName = "PC2" });
            content.Add(new ProductPartRelation() { ProductName = "P2", PartType = PartType.ProbeCard, PartName = "PC3" });
            content.Add(new ProductPartRelation() { ProductName = "P2", PartType = PartType.Sensor, PartName = "S1" });

            content.Add(new ProductPartRelation() { ProductName = "P3", PartType = PartType.ProbeCard, PartName = "PC2" });
            content.Add(new ProductPartRelation() { ProductName = "P3", PartType = PartType.ProbeCard, PartName = "PC3" });
            content.Add(new ProductPartRelation() { ProductName = "P3", PartType = PartType.Sensor, PartName = "S2" });

            content.Add(new ProductPartRelation() { ProductName = "P4", PartType = PartType.ProbeCard, PartName = "PC1" });
            content.Add(new ProductPartRelation() { ProductName = "P4", PartType = PartType.ProbeCard, PartName = "PC3" });
            content.Add(new ProductPartRelation() { ProductName = "P4", PartType = PartType.Sensor, PartName = "S1" });
            content.Add(new ProductPartRelation() { ProductName = "P4", PartType = PartType.Sensor, PartName = "S3" });

            content.Add(new ProductPartRelation() { ProductName = "P5", PartType = PartType.ProbeCard, PartName = "PC1" });
            content.Add(new ProductPartRelation() { ProductName = "P5", PartType = PartType.ProbeCard, PartName = "PC2" });
            content.Add(new ProductPartRelation() { ProductName = "P5", PartType = PartType.ProbeCard, PartName = "PC3" });
            content.Add(new ProductPartRelation() { ProductName = "P5", PartType = PartType.Sensor, PartName = "S1" });
            content.Add(new ProductPartRelation() { ProductName = "P5", PartType = PartType.Sensor, PartName = "S2" });
            content.Add(new ProductPartRelation() { ProductName = "P5", PartType = PartType.Sensor, PartName = "S3" });

            content.Add(new ProductPartRelation() { ProductName = "P6", PartType = PartType.ProbeCard, PartName = "PC1" });
            content.Add(new ProductPartRelation() { ProductName = "P6", PartType = PartType.Sensor, PartName = "S2" });
            content.Add(new ProductPartRelation() { ProductName = "P6", PartType = PartType.Sensor, PartName = "S3" });
            this.Content = content;
        }

        public ProductPartRelation Read(Predicate<ProductPartRelation> find)
        {
            return this.Content.Find(find);
        }

        public IEnumerable<ProductPartRelation> ReadAll()
        {
            return this.Content;
        }

        public IEnumerable<ProductPartRelation> Reads(Predicate<ProductPartRelation> find)
        {
            return this.Content.FindAll(find);
        }
    }

    class MockPart : IRepository<Part>
    {
        List<Part> Parts;

        public MockPart()
        {
            var parts = new List<Part>();
            parts.Add(new Part(PartType.ProbeCard, "PC1", 5));
            parts.Add(new Part(PartType.ProbeCard, "PC2", 2));
            parts.Add(new Part(PartType.ProbeCard, "PC3", 1));
            parts.Add(new Part(PartType.Sensor, "S1", 1));
            parts.Add(new Part(PartType.Sensor, "S2", 2));
            parts.Add(new Part(PartType.Sensor, "S3", 3));
            this.Parts = parts;
        }

        public Part Read(Predicate<Part> find)
        {
            return this.Parts.Find(find);
        }

        public IEnumerable<Part> ReadAll()
        {
            return this.Parts;
        }

        public IEnumerable<Part> Reads(Predicate<Part> find)
        {
            return this.Parts.FindAll(find);
        }
    }

    class MockMCPlan : IRepository<ProductEqpPlan>
    {
        List<ProductEqpPlan> Plans;

        public MockMCPlan()
        {
            var plans = new List<ProductEqpPlan>();
            plans.Add(new ProductEqpPlan() { ProductName = "P1", EqpCount = 2 });
            plans.Add(new ProductEqpPlan() { ProductName = "P3", EqpCount = 1 });
            plans.Add(new ProductEqpPlan() { ProductName = "P3", EqpCount = 1 });
            plans.Add(new ProductEqpPlan() { ProductName = "P1", EqpCount = 1 });
            this.Plans = plans;
        }

        public ProductEqpPlan Read(Predicate<ProductEqpPlan> find)
        {
            return this.Plans.Find(find);
        }

        public IEnumerable<ProductEqpPlan> ReadAll()
        {
            return this.Plans;
        }

        public IEnumerable<ProductEqpPlan> Reads(Predicate<ProductEqpPlan> find)
        {
            return this.Plans.FindAll(find);
        }
    }
}
