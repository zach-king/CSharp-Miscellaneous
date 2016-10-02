using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSandbox
{
    /* Takes a postfix expression from console and 
    outputs the computed value */
    class PostfixEvaluator
    {
        private bool isOperator(string token)
        {
            return (token == "+" ||
                token == "-" ||
                token == "*" ||
                token == "/");
        }

        private double calculate(double op1, double op2, string oper)
        {
            switch (oper)
            {
                case "+":
                    return op1 + op2;
                case "-":
                    return op2 - op1;
                case "*":
                    return op1 * op2;
                case "/":
                    return op2 / op1;
                default:
                    return 0;
            }
        } 

        public Tuple<bool, double> ComputeExpression(string exp)
        {
            // Stack for holding operands
            Stack<double> stack = new Stack<double>();

            // Split into tokens
            string[] tokens = exp.Split(' ');

            // Parse each token
            foreach (string token in tokens)
            {
                // If not an operator, put on stack
                if (!isOperator(token))
                {
                    stack.Push(Convert.ToDouble(token));
                }
                else
                {
                    // Token is operator so check if valid (enough ops on stack)
                    // and calculate, and push result on stack
                    if (stack.Count < 2)
                    {
                        Console.Write("Invalid Postfix Expression: " + exp + "\nFailed at token `" + token + "`\n");
                        return new Tuple<bool, double>(false, 0);
                    }

                    double op1 = stack.Pop();
                    double op2 = stack.Pop();
                    stack.Push(calculate(op1, op2, token));
                }
            }
            if (stack.Count == 1)
                return new Tuple<bool, double>(true, stack.Pop());
            return new Tuple<bool, double>(false, 0);
        } 
    }


    class Test
    {
        static void Main(string[] args)
        {
            PostfixEvaluator calc = new PostfixEvaluator();

            // Read in the expression
            string expression = Console.In.ReadLine();

            // Get result of expression 
            Tuple<bool, double> result = calc.ComputeExpression(expression);

            if (result.Item1)
            {
                Console.Write("Result: ");
                Console.Write(result.Item2);
                Console.WriteLine();
            }
            else
            {
                Console.Write("--ERROR--\n");
            }
        }
    }
}
