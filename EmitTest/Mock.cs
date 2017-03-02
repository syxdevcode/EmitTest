using System;
using System.Linq.Expressions;
using System.Reflection;

namespace EmitTest
{
    public class Mock<T> where T : IAssessmentAopAdviceProvider
    {
        public T Obj
        {
            get; set;
        }

        public SetupContext Contex { get; set; }

        public Mock()
        {
        }
    }

    public class SetupContext
    {
        public MethodInfo MethodInfo { get; set; }
    }

    public static class MockExtention
    {
        public static Mock<T> Setup<T>(this Mock<T> mocker, Expression<Action<T>> expression) where T : IAssessmentAopAdviceProvider
        {
            mocker.Contex = new SetupContext();

            mocker.Contex.MethodInfo = expression.ToMethodInfo();

            return mocker;
        }

        public static void Returns<T>(this Mock<T> mocker, object returnValue) where T : IAssessmentAopAdviceProvider
        {
            if (mocker.Contex != null && mocker.Contex.MethodInfo != null)
            {
                //这里为简单起见，只考虑IAssessmentAopAdviceProvider接口
                mocker.Obj = (T)AdviceProviderFactory.GetProvider(mocker.Contex.MethodInfo.Name, (string)returnValue);
            }
        }

        public static MethodInfo ToMethodInfo(this LambdaExpression expression)
        {
            var memberExpression = expression.Body as System.Linq.Expressions.MethodCallExpression;

            if (memberExpression != null)

            {
                return memberExpression.Method;
            }

            return null;
        }
        
    }
}