using System;
using System.Reflection;
using System.Reflection.Emit;

namespace EmitTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //AssemblyName assemblyName = new AssemblyName("EmitTest.MvcAdviceProvider");

            //AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);

            //ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MvcAdviceProvider");

            //TypeBuilder typeBuilder = moduleBuilder.DefineType("EmitTest.MvcAdviceProvider", TypeAttributes.Public,

            //                                                   typeof(object), new Type[] { typeof(IAssessmentAopAdviceProvider) });

            //MethodBuilder beforeMethodBuilder = typeBuilder.DefineMethod("Before", MethodAttributes.Public, typeof(object), new Type[] { typeof(object) });

            //MethodBuilder afterMethodBuilder = typeBuilder.DefineMethod("After", MethodAttributes.Public, typeof(object), new Type[] { typeof(object), typeof(object) });

            //TestType(typeBuilder);


            Mock<IAssessmentAopAdviceProvider> mocker = new Mock<IAssessmentAopAdviceProvider>();

            mocker.Setup(t => t.Before(3)).Returns("Hello World!");

            Console.WriteLine(mocker.Obj.Before(2));

            Console.Read();
        }

        private static void TestType(TypeBuilder typeBuilder)
        {
            Console.WriteLine(typeBuilder.Assembly.FullName);

            Console.WriteLine(typeBuilder.Module.Name);

            Console.WriteLine(typeBuilder.Namespace);

            Console.WriteLine(typeBuilder.Name);

            Console.Read();
        }
    }
}