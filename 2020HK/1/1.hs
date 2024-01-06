resolveLinesRec :: [Int] -> Int -> Int -> Int
resolveLinesRec [] _ _ = 0
resolveLinesRec _  0 0 = 1
resolveLinesRec _  _ 0 = 0

resolveLinesRec (x:xs) targetVal n=
    if previous  >0
        then  x*previous
        else resolveLinesRec xs targetVal n

        where previous=resolveLinesRec xs (targetVal-x) (n-1)

part1 llines=
    show  $ resolveLinesRec (map read llines) 2020 2

part2 llines=
    show  $ resolveLinesRec (map read llines) 2020 3

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