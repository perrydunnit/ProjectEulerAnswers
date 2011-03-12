using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Linq;
using ProjectEuler.Solutions;

namespace ProjectEuler
{
    public class EulerCommandLine
    {
    	private Tuple<int, int> _problemToSolve;
    	private readonly CompositionContainer _container;

    	public EulerCommandLine()
    	{
    		var catalog = new AggregateCatalog();

			catalog.Catalogs.Add(new AssemblyCatalog(typeof(IEulerProblemSolution).Assembly));

			_container = new CompositionContainer(catalog);

			//Fill the imports of this object
			try
			{
				_container.ComposeParts(this);
			}
			catch (CompositionException compositionException)
			{
				Console.WriteLine(compositionException.ToString());
			}

    	}

        private List<IEulerProblemSolution> EulerProblemSolutions
        {
			get
			{
				List<IEulerProblemSolution> eulerProblemSolutions = Solutions.ToList();
				eulerProblemSolutions.Sort((a, b) =>
										   a.ProblemNumber.CompareTo(b.ProblemNumber) != 0
											   ? a.ProblemNumber.CompareTo(b.ProblemNumber)
											   : a.ProblemVersion.CompareTo(b.ProblemVersion));
				return eulerProblemSolutions;
			}
        }
		//private List<IEulerProblemSolution> EulerProblemSolutions
		//{
		//    get
		//    {
		//        if (_eulerProblemSolutions == null)
		//        {
		//            List<Type> solutionTypes = TypesImplementingInterface(typeof (IEulerProblemSolution));


		//            _eulerProblemSolutions = solutionTypes.FindAll(
		//                t => t.IsClass).ConvertAll(t =>
		//                                            {
		//                                                ConstructorInfo constructorInfo = t.GetConstructor(new Type[0]);
		//                                                return constructorInfo != null
		//                                                        ? constructorInfo.Invoke(null) as IEulerProblemSolution
		//                                                        : null;
		//                                            });
		//            _eulerProblemSolutions.Sort((a, b) =>
		//                                        a.ProblemNumber.CompareTo(b.ProblemNumber) != 0
		//                                            ? a.ProblemNumber.CompareTo(b.ProblemNumber)
		//                                            : a.ProblemVersion.CompareTo(b.ProblemVersion));
		//        }
		//        return _eulerProblemSolutions;
		//    }
		//}

        public void Start(string[] args)
        {
            if (args.Length == 0 || !TryGetProblemAndVersion(args[0], out _problemToSolve))
            {
                DisplayProblems();
                DetermineProblemToSolve();
            }
        }

        private static bool TryGetProblemAndVersion(string request, out Tuple<int, int> problemAndVersion)
        {
            if (!request.Contains("."))
            {
                problemAndVersion = null;
                return false;
            }
            int problemNumber;
            int version;
            string[] requestArray = request.Split('.');
            if (
                !int.TryParse(requestArray[0], out problemNumber) ||
                !int.TryParse(requestArray[1], out version))
            {
                problemAndVersion = null;
                return false;
            }

            problemAndVersion = new Tuple<int, int>(problemNumber, version);
            return true;
        }

        private void SolveProblem()
        {
            IEulerProblemSolution solutionToRun =
                EulerProblemSolutions.Find(
                    s => s.ProblemNumber == _problemToSolve.Item1 && s.ProblemVersion == _problemToSolve.Item2);

            Console.WriteLine("Solving problem{0} with solution version {1} ({2})",
                              solutionToRun.ProblemNumber,
                              solutionToRun.ProblemVersion,
                              solutionToRun.Description);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            string solution = solutionToRun.ComputeAnswer();
            stopwatch.Stop();
            string timeAsString = TimeAsString(stopwatch.Elapsed);
            Console.WriteLine("Solution found in {0}" + ": {1}", timeAsString, solution);

            Console.WriteLine();
            _problemToSolve = null;
            DetermineProblemToSolve();
        }

        private static string TimeAsString(TimeSpan timeTaken)
        {
            if (timeTaken.Days > 1)
            {
                return timeTaken.TotalDays + " days";
            }
            if (timeTaken.Hours > 1)
            {
                return timeTaken.TotalHours + " hours";
            }
            if (timeTaken.TotalMinutes > 1)
            {
                return timeTaken.TotalMinutes + " minutes";
            }
            if (timeTaken.TotalSeconds > 1)
            {
                return timeTaken.TotalSeconds + " seconds";
            }
            return timeTaken.TotalMilliseconds + " milliseconds";
        }

		[ImportMany]
		private IEnumerable<IEulerProblemSolution> Solutions { get; set; }


    	private void DetermineProblemToSolve()
        {
            string response = Console.ReadLine();
            if (response != String.Empty)
            {
                CheckForProblemToSolve(response);
            }
            if (_problemToSolve != null)
            {
                SolveProblem();
            }
        }

        private void DisplayProblems()
        {
            Console.WriteLine(
                "The following solutions are available.  Please enter the problem number for which you'd like the solution:");
            EulerProblemSolutions.ForEach(
                x => Console.WriteLine("{0}.{1}: {2}", x.ProblemNumber, x.ProblemVersion, x.Description));
            Console.WriteLine("To exit, simply press <Enter>");
        }

        private void CheckForProblemToSolve(string response)
        {
            if (!TryGetProblemAndVersion(response, out _problemToSolve))
            {
                DetermineProblemToSolve();
            }
        }
    }
}