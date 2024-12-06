

using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Day6Lib
{
    public class MapParser
    {
        public static List<List<char>> ParseMap(List<string> input)
        {
            return input.Select(x => x.ToCharArray().ToList()).ToList();
        }
    }

    public class MapBoard
    {
        public readonly List<List<char>> _Map;
        public (int guardY, int guardX, char guardDirection) _guardPosition;
        public (int guardY, int guardX) _originalguardPosition;
        private bool guardIsOut = false;
        private HashSet<(int, int, char)> visitedLocations = [];
        private HashSet<char> GuardPossible = ['<', '>', '^', 'v'];

        public MapBoard(List<List<char>> startingPosition)
        {
            _Map = startingPosition;
            _guardPosition = FindGuard();

            RemoveGuardFromMap();
        }

        public MapBoard(List<List<char>> startingPosition, (int guardY, int guardX, char guardDirection) guardPosition)
        {
            _Map = startingPosition;
            _guardPosition = guardPosition;
            _originalguardPosition = (guardPosition.guardY,  guardPosition.guardX);
        }

        private void RemoveGuardFromMap()
        {
            for (int y = 0; y < _Map.Count; y++)
            {
                for (int x = 0; x < _Map[y].Count; x++)
                {

                    if (GuardPossible.Contains(_Map[y][x]))
                    {
                        _Map[y][x] = '.';
                    }
                }
            }
        }

        public bool GuardIsInside => !guardIsOut;

        public void StepGuard()
        {
            var newPossibleGuardPosition = _guardPosition.guardDirection switch
            {
                '<' => (_guardPosition.guardY, _guardPosition.guardX - 1),
                '>' => (_guardPosition.guardY, _guardPosition.guardX + 1),
                '^' => (_guardPosition.guardY - 1, _guardPosition.guardX),
                'v' => (_guardPosition.guardY + 1, _guardPosition.guardX),
            };
            if (newPossibleGuardPosition.Item1 < 0 || newPossibleGuardPosition.Item1 >= _Map.Count || newPossibleGuardPosition.Item2 < 0 || newPossibleGuardPosition.Item2 >= _Map[0].Count)
            {
                visitedLocations.Add(_guardPosition);
                guardIsOut = true;
                return;
            };
            var thingInfront = _Map[newPossibleGuardPosition.Item1][newPossibleGuardPosition.Item2];
            if (thingInfront == '.')
            {
                visitedLocations.Add(_guardPosition);
                _guardPosition.guardY = newPossibleGuardPosition.Item1;
                _guardPosition.guardX = newPossibleGuardPosition.Item2;
            }
            else
            {
                _guardPosition.guardDirection = TurnRight(_guardPosition.guardDirection);
            }
        }

        private char TurnRight(char guardDirection)
        {
            return guardDirection switch
            {
                '<' => '^',
                '>' => 'v',
                '^' => '>',
                'v' => '<',
            };
        }

        private (int guardY, int guardX, char guardDirection) FindGuard()
        {
            var possible = new List<char> { '<', '>', '^', 'v' };
            for (int y = 0; y < _Map.Count; y++)
            {
                for (int x = 0; x < _Map[y].Count; x++)
                {
                    if (possible.Contains(_Map[y][x]))
                    {
                        return (y, x, _Map[y][x]);
                    }
                }
            }
            throw new Exception("guard not found");
        }

        public List<(int, int)> GetWalkedPositions()
        {
            return visitedLocations.Select(x => (x.Item1, x.Item2)).ToList();
        }

        public void Print()
        {
            for (int y = 0; y < _Map.Count; y++)
            {
                for (int x = 0; x < _Map[y].Count; x++)
                {
                    if (_guardPosition.guardY == y && _guardPosition.guardX == x)
                    {
                        Console.Write(_guardPosition.guardDirection);
                    }
                    else
                    {
                        if (GuardPossible.Contains(_Map[y][x]))
                        {
                            Console.Write('.');
                        } 
                        else
                        {
                            Console.Write(_Map[y][x]);
                        }
                    }
                }
                Console.WriteLine();
            }
        }

        internal static MapBoard CloneFromWithObstruction(MapBoard input, int y, int x, (int guardY, int guardX, char guardDirection) originalGuardLocation)
        {
            var copy = input._Map.Select(x => x.ToList()).ToList();
            copy[y][x] = 'O';
            return new MapBoard(copy, originalGuardLocation);
        }

        internal bool GuardIsInLoop()
        {
            return visitedLocations.Contains(_guardPosition);
        }
    }
}
