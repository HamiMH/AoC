

linesToInts[]=[]
linesToInts (x:xs)=(read x) : linesToInts xs

resolveLines x=show x

part1 lines=
    resolveLines (linesToInts lines)
    "part1"
part2 lines="part2"


main = do
  input <- readFile "inT"
  print $ part1 $ lines input
  print $ part2 $ lines input