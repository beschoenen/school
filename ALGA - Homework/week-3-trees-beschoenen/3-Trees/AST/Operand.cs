using System;
using System.Collections.Generic;
using System.Linq;
namespace ALGA
{
    public class Operand : Expression
    {
        private readonly char _operand;
        private readonly Expression _left;
        private readonly Expression _right;

        public Operand(char operand, Expression left, Expression right)
        {
            _operand = operand;
            _left = left;
            _right = right;
        }

        public override int evaluate()
        {
            var left = _left.evaluate();
            var right = _right.evaluate();

            switch (_operand)
            {
                case '*':
                    return left * right;
                case '/':
                    return left / right;
                case '+':
                    return left + right;
                case '-':
                    return left - right;
                default:
                    throw new Exception("Invalid Operator");
            }
        }

        public override string ToString()
        {
            return $"({_left}{_operand}{_right})";
        }
    }
}
