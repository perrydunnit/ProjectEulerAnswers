using System;
using System.Collections.Generic;
using System.Linq;
using ProjectEuler.Solutions;

namespace ProjectEuler
{
    public class EulerCommandLine
    {
        private List<IEulerProblemSolution> _eulerProblemSolutions;
        private int _problemToSolve;

        public void Start(string[] args)
        {
            if (args.Length == 0 || !Int32.TryParse(args[0], out _problemToSolve))
            {
                DetermineProblemToSolve();
            }

            SolveProblem(_problemToSolve);
        }

        private List<IEulerProblemSolution> EulerProblemSolutions
        {
            get
            {
                if (_eulerProblemSolutions==null)
                {
                    List<Type> solutionTypes = TypesImplementingInterface(typeof(IEulerProblemSolution));

                    _eulerProblemSolutions = solutionTypes.FindAll(t=>t.IsClass).ConvertAll(t =>
                                                                          {
                                                                              var constructorInfo = t.GetConstructor(new Type[0]);
                                                                              return constructorInfo != null ? constructorInfo.Invoke(null) as IEulerProblemSolution : null;
                                                                          });
                }
                return _eulerProblemSolutions;
            }
        }

        private void SolveProblem(int problemToSolve)
        {
            IEulerProblemSolution solutionToRun = EulerProblemSolutions.Find(s => s.ProblemNumber == problemToSolve);

            Console.WriteLine("Solving problem{0} ({1})", problemToSolve, solutionToRun.Description);
            DateTime startTime = DateTime.Now;
            string solution = solutionToRun.ComputeAnswer();
            DateTime endTime = DateTime.Now;
            Console.WriteLine("Solution found in {0} seconds: {1}",(endTime-startTime).TotalSeconds, solution);
        }

        public List<Type> TypesImplementingInterface(Type desiredType)
        {
            return AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(desiredType.IsAssignableFrom).ToList();

        }


        private void DetermineProblemToSolve()
        {
            Console.WriteLine(
                "No problem was indicated.  The following are available.  Please enter the problem number for which you'd like the solution:");

            EulerProblemSolutions.ForEach(x => Console.WriteLine("{0}: {1}", x.ProblemNumber, x.Description));


            string response = Console.ReadLine();
            CheckForProblemToSolve(response);
        }

        private void CheckForProblemToSolve(string response)
        {
            if (!Int32.TryParse(response, out _problemToSolve))
            {
                DetermineProblemToSolve();
            }
        }

    }
}