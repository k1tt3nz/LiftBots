using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
	public class ProductConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return typeof(Product).IsAssignableFrom(objectType);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JObject jsonObject = JObject.Load(reader);
			string name = jsonObject["Name"].ToString();
			int type = int.Parse(jsonObject["Type"].ToString());
			if (type == 1)
			{
				return new CreatorA().FactoryMethod(name,type);
			}
			else if (type == 2)
			{
				return new CreatorB().FactoryMethod(name, type);
			}
			throw new JsonSerializationException("Invalid product type");
		}
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			Product product = (Product)value;
			JObject jsonObject = new JObject
			{
				{ "Name", product.Name },
				{ "Type", product.Type }
			};
			jsonObject.WriteTo(writer);
		}
	}
	interface IProduct
	{
		const int type1 = 1;
		const int type2 = 2;
		void PrintType();
	}
	public abstract class Product : IProduct
	{
		public string Name { get; set; }
		public int Type { get; set; }
		public abstract void PrintType();

		public Product(string name, int type) 
		{
			Name = name;
			Type = type;
		}
	}

	public class A : Product
	{
		public A(string name, int type) : base(name, type) { }

		public override void PrintType()
		{
            Console.WriteLine("Class A");
        }
	}

	public class B : Product
	{
		public B(string name, int type) : base(name, type) { }
		public override void PrintType()
		{
			Console.WriteLine("Class B");
		}
	}

	public abstract class Creator
	{
		public abstract Product FactoryMethod(string name, int type);
	}

	public class CreatorA : Creator
	{
		public override Product FactoryMethod(string name, int type)
		{
			return new A(name,type);
		}
	}

	public class CreatorB : Creator
	{
		public override Product FactoryMethod(string name, int type)
		{
			return new B(name,type);
		}
	}
}