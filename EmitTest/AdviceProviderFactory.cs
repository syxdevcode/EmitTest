using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace EmitTest
{
    public static class AdviceProviderFactory
    {
        private static Dictionary<string, IAssessmentAopAdviceProvider> instanceDic;

        private static readonly AssemblyName assemblyName = new AssemblyName("EmitTest.MvcAdviceProvider");

        private static AssemblyBuilder assemblyBuilder;

        private static ModuleBuilder moduleBuilder;

        static AdviceProviderFactory()
        {
            assemblyName.Version = new Version("1.0.0.0");

            assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);

            moduleBuilder = assemblyBuilder.DefineDynamicModule("MvcAdviceProviderModule", "test.dll", true);
        }

        public static IAssessmentAopAdviceProvider GetProvider(string methodName, string returnValue) 
        {
            //创建接口的实例
            return CreateInstance("MvcAdviceReportProviderInstance", methodName, returnValue);
        }

        private static IAssessmentAopAdviceProvider CreateInstance(string instanceName, string methodName, string returnValue)
        {
            TypeBuilder typeBuilder = moduleBuilder.DefineType("MvcAdviceProvider.MvcAdviceProviderType", TypeAttributes.Public, typeof(object), new Type[] { typeof(IAssessmentAopAdviceProvider) });

            // typeBuilder.AddInterfaceImplementation(typeof(IAssessmentAopAdviceProvider));

            MethodBuilder beforeMethodBuilder = typeBuilder.DefineMethod(methodName, MethodAttributes.Public | MethodAttributes.Virtual, typeof(string), new Type[] { typeof(int) });

            beforeMethodBuilder.DefineParameter(1, ParameterAttributes.None, "value");

            ILGenerator generator1 = beforeMethodBuilder.GetILGenerator();

            LocalBuilder local1 = generator1.DeclareLocal(typeof(string));

            local1.SetLocalSymInfo("param1");

            generator1.Emit(OpCodes.Nop);

            generator1.Emit(OpCodes.Ldstr, returnValue);

            generator1.Emit(OpCodes.Stloc_0);

            generator1.Emit(OpCodes.Ldloc_0);

            generator1.Emit(OpCodes.Ret);

            Type providerType = typeBuilder.CreateType();

            assemblyBuilder.Save("test.dll");

            IAssessmentAopAdviceProvider provider = Activator.CreateInstance(providerType) as IAssessmentAopAdviceProvider;

            return provider;
        }
    }
}