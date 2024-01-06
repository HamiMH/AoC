numberOfHt :: [String] -> Int -> Int-> Int -> Int -> Integer
numberOfHt [] _ _ _ _=0
numberOfHt llines y x slopeY slopeX   | y>= lenY =0
                        | (llines!!y!!x)=='#'   = 1 + numberOfHt llines newY newX slopeY slopeX
                        | (llines!!y!!x)=='.'   = 0 + numberOfHt llines newY newX slopeY slopeX
                        | otherwise =0
                        where lenX=length $ head llines
                              lenY=length $ llines
                              newX=mod (x+slopeX) lenX
                              newY=y+slopeY

runWithSlopes :: [String] -> [[Int]] -> Integer
runWithSlopes _ []= 1
runWithSlopes llines ([x,y]:xs)=
                    runWithSlopes llines xs * numberOfHt llines 0 0  y x


part1 llines=
  numberOfHt llines 0 0 1 3

part2 llines= show$ runWithSlopes llines [[1,1],[3,1],[5,1],[7,1],[1,2]]

main = do

  inputTest <- readFile "inT"
  inputReal <- readFile "inR"
  putStrLn "Part1: test"
  print $ part1 $ lines inputTest
  putStrLn "Part1: real"
  print $ part1 $ lines inputReal

  putStrLn "Part2: test"
  print $ part2 $ lines inputTest
  putStrLn "Part2: real"
  print $ part2 $ lines inputReal