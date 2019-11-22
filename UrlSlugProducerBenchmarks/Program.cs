using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace UrlSlugBenchmarks
{
    [MemoryDiagnoser]
    public class Program
    {
        private readonly SlugProducerBase slugProducerV1 = new SlugProducerV1();
        private readonly SlugProducerBase slugProducerV2 = new SlugProducerV2();
        private readonly SlugProducerBase slugProducerV2_2 = new SlugProducerV22();

        [Params(
            "abcdegfhijklmnopqrstuvwxyz0123456789 .-–.- -",
            " .-–.- -ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789",
            "@#a$@bc%d&^*^%&*EF$%^&GHI^&*&^J()_KLMNOPQRSTUVWXYZ0123456789&^*)",
            " @#a$ @bc%.d&^*" + "–" + "^%&*EF$%^&GHI^&*&^J()_KLMNOPQRS TUVWXYZ0123456789&^*)" + "A B.C-D" + " .-" + "–" + "ABCDEFGHIJKLMN" + "–" + "OPQRSTUVWXYZ0123456789 .- –")]
        public string Scenario { get; set; }

        static void Main(string[] args)
        {           
            BenchmarkRunner.Run<Program>();
        }

        [Benchmark(Baseline = true)]
        public string SlugProducerV1()
        {
            return slugProducerV1.GetUrlSlug(Scenario);
        }

        [Benchmark]
        public string SlugProducerV2()
        {
            return slugProducerV2.GetUrlSlug(Scenario);
        }

        [Benchmark]
        public string SlugProducerV2_2()
        {
            return slugProducerV2_2.GetUrlSlug(Scenario);
        }
    }
}
