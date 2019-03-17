using System;
using Bogus;
using Bogus.DataSets;
using Newtonsoft.Json;

namespace Sample
{
    class Program
    {
        class User
        {
            /// <summary>
            /// 名字
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 年龄
            /// </summary>
            public int Age { get; set; }

            /// <summary>
            /// 性别
            /// </summary>
            /// <remarks>这里使用Bogus已经定义好的性别枚举类型</remarks>
            public Name.Gender Gender { get; set; }

            /// <summary>
            /// 公司
            /// </summary>
            public string Company { get; set; }

            /// <summary>
            /// 电话
            /// </summary>
            public string Phone { get; set; }
        }

        static void Main(string[] args)
        {
            // 用户数据生成规则
            var fakerPerson = new Faker<User>("zh_CN")                                              // 使用中文数据
                    .RuleFor(p => p.Name, f => f.Name.FullName())                           // 随机汉字名
                    .RuleFor(p => p.Age, f => f.Random.Number(1, 100))                      // 随机年龄(1-100岁)
                    .RuleFor(p => p.Gender, f => f.PickRandom<Name.Gender>())               // 随机性别
                    .RuleFor(p => p.Company, p => p.Company.CompanyName())                  // 随机公司名称
                    .RuleFor(p => p.Phone, p => p.Phone.PhoneNumber("1##########"))     // 随机手机号
                ;
            // 生成测试用户
            var person = fakerPerson.Generate();
            // 输出测试用户数据
            string json = JsonConvert.SerializeObject(person, Formatting.Indented);
            Console.WriteLine(json);
        }
    }
}