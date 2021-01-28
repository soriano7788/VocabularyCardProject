using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using NLog;
using System.Transactions;

namespace VocabularyCard.Core.Interceptors
{
    public class TransactionInterceptor : IInterceptor
    {
        public static readonly Logger Logger = LogManager.GetLogger("TransactionInterceptor");

        private IUnitOfWork _unitOfWork;
        private string[] _transactionMethodsPrefix;
        private string[] _transactionMethods;
        private string[] _ignoreTransactionMethods;

        public string[] TransactionMethodsPrefix
        {
            set 
            {
                _transactionMethodsPrefix = value;
            }
        }
        public string[] TransactionMethods
        {
            set
            {
                _transactionMethods = value;
            }
        }
        public string[] IgnoreTransactionMethods
        {
            set
            {
                _ignoreTransactionMethods = value;
            }
        }

        public TransactionInterceptor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Intercept(IInvocation invocation)
        {
            Type type = invocation.TargetType;

            object[] args = invocation.Arguments;
            MethodInfo method = invocation.Method;
            string methodName = method.Name;

            try
            {

                Logger.Debug("start " + type.Name + " " + methodName);

                invocation.Proceed();

                Logger.Debug("finish " + type.Name + " " + methodName);

                if(RequireTransaction(methodName) && !IsIgnoreTransactionMethod(methodName))
                {
                    // todo: 假如相近的時間內有兩個 request 進來，且都要 commit，這邊有高機率炸掉
                    // 之前沒過濾 get 相關 methods 時就遇到了，
                    // 需要注意 看一下如何解....
                    // 似乎與 autofac 的 register 相關，還需要多了解 autofac

                    // 還有一個疑惑，假如我在 get 相關的 methods 裡面都 呼叫 _unitOfWork.Save()
                    // 是不是就能重現這邊遇到的問題了

                    // autofac register 是 singleton 或是 perRequest 等都會影響

                    // 目前把除了 Map 檔以外的都用 InstancePerLifetimeScope，衝突的情況似乎就沒了(先把 get* methods 納入 transaction 來測試)


                    Logger.Debug(type.Name + " " + methodName + " need commit");
                    _unitOfWork.Save();
                }

            }
            catch (Exception e)
            {
                Logger.Error(type.Name + " " + methodName + " TransactionInterceptor error: " + e);
                throw;
            }
            finally
            {
            }
        }

        private bool RequireTransaction(string methodName)
        {
            foreach (string prefix in _transactionMethodsPrefix)
            {
                if(methodName.StartsWith(prefix))
                {
                    return true;
                }
            }
            foreach(string method in _transactionMethods)
            {
                if(methodName.Equals(method))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsIgnoreTransactionMethod(string methodName)
        {
            foreach (string ignoreName in _ignoreTransactionMethods)
            {
                if (methodName == ignoreName)
                {
                    return true;
                }
            }
            return false;
        }

    }
};