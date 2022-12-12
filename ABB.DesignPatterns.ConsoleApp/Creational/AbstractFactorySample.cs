using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABB.DesignPatterns.ConsoleApp.Creational
{
   //AbstractProductA 
   public abstract class AccountsProfessional {  }

    //ProductA1
    public class AccountsAssistant : AccountsProfessional {  }
    //ProductA2 
    public class FinanceManager : AccountsProfessional { }


    //AbstractProductB
    public abstract class SoftwareProfessional
    {
        public abstract void InteractsWith(AccountsProfessional professional); 
    }
    //ProductB1
    public class Developer: SoftwareProfessional
    {
        public override void InteractsWith(AccountsProfessional professional)
        {
            Console.WriteLine($"{this.GetType().Name} interacts with {professional.GetType().Name} for Salary Planning");
        }
    }
    //ProductB2
    public class ProjectManager : SoftwareProfessional
    {
        public override void InteractsWith(AccountsProfessional professional)
        {
            Console.WriteLine($"{this.GetType().Name} interacts with {professional.GetType().Name} for Business Planning");
        }
    }

    //AbstractFactory 
    public abstract class PlanningFactory
    {
        public abstract AccountsProfessional GetAccountsProfessional();
        public abstract SoftwareProfessional GetSoftwareProfessional(); 
    }
    //ConcreteFactory1
    public class SalaryPlanningFactory : PlanningFactory
    {
        public override AccountsProfessional GetAccountsProfessional()
        {
            return new AccountsAssistant();
        }
        public override SoftwareProfessional GetSoftwareProfessional()
        {
            return new Developer();
        }
    }
    //ConcreteFactory2
    public class BusinessPlanningFactory : PlanningFactory
    {
        public override AccountsProfessional GetAccountsProfessional()
        {
            return new FinanceManager();
        }
        public override SoftwareProfessional GetSoftwareProfessional()
        {
            return new ProjectManager();
        }
    }

    public enum FactoryTypes {  SalaryPlanning, BusinessPlanning }

    //Client 
    public class AbstractFactoryClient
    {
        internal static PlanningFactory Create(FactoryTypes type)
        {
            PlanningFactory pf = null;
            if (type == FactoryTypes.BusinessPlanning)
                pf = new BusinessPlanningFactory();
            else if (type == FactoryTypes.SalaryPlanning)
                pf = new SalaryPlanningFactory();

            return pf;
        }
        internal static void Test()
        {
            //var pf1 = new SalaryPlanningFactory();
            var pf1 = AbstractFactoryClient.Create(FactoryTypes.SalaryPlanning);
            var accProf1 = pf1.GetAccountsProfessional();
            var swProf1 = pf1.GetSoftwareProfessional();
            swProf1.InteractsWith(accProf1);
            //var pf2 = new BusinessPlanningFactory();
            var pf2 = AbstractFactoryClient.Create(FactoryTypes.BusinessPlanning);
            var accProf2 = pf2.GetAccountsProfessional();
            var swProf2 = pf2.GetSoftwareProfessional();
            swProf2.InteractsWith(accProf2);
        }
    }
}
