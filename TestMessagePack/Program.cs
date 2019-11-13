using System;
using MessagePack;

namespace TestMessagePack
{
    class Program
    {
        static void Main(string[] args)
        {
            var myClass = new MyClass
            {
                Age = 99,
                FirstName = "hoge",
                LastName = "huga",
            };

            // call Serialize/Deserialize, that's all.
            byte[] bytes = MessagePackSerializer.Serialize(myClass);
            MyClass mc2 = MessagePackSerializer.Deserialize<MyClass>(bytes);

            // you can dump msgpack binary to human readable json.
            // In default, MeesagePack for C# reduce property name information.
            // [99,"hoge","huga"]
            var json = MessagePackSerializer.ToJson(bytes);
            Console.WriteLine(json);
        }
    }

    [MessagePackObject]
    public class MyClass
    {
        // Key is serialization index, it is important for versioning.
        [Key(0)]
        public int Age { get; set; }

        [Key(1)]
        public string FirstName { get; set; }

        [Key(2)]
        public string LastName { get; set; }

        // public members and does not serialize target, mark IgnoreMemberttribute
        [IgnoreMember]
        public string FullName { get { return FirstName + LastName; } }
    }
}