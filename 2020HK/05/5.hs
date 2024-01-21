import Data.List (sort)


convertStrToListId :: String -> Int
convertStrToListId strId= valueId
                  where valueId=row*8+col
                        row=convRow strRow 0 127
                        col=convRow strCol 0 7
                        strRow=take 7 strId
                        strCol=take 3 $ drop 7 strId


convRow :: [Char] ->Int -> Int -> Int
convRow [] from to=from
convRow (x:xs) from to  | from==to =from
                        | x=='F'||x=='L' =convRow xs from (div (from+to)  2)
                        | x=='B'||x=='R' =convRow xs ((div (from+to)2)+1) to



part1 input=show $ maximum listSeatId
          where listSeatId=map convertStrToListId input
part2 input= show $ findMissing listSeatIdSorted
          where   listSeatId=map convertStrToListId input
                  listSeatIdSorted = sort listSeatId

findMissing :: [Int] -> Int
findMissing []= 0
findMissing (x1:x2:[])= 0
findMissing (x1:x2:xs)  |x2-x1==2 = (x1+1)
                        |otherwise= findMissing (x2:xs)

main = do

  inputTest <- readFile "inT"
  inputReal <- readFile "inR"
  putStrLn "Part1: test"
  print $ part1 $ lines inputTest
  putStrLn "Part1: real"
  print $ part1 $ lines  inputReal

  putStrLn "Part2: test"
  print $ part2 $ lines inputTest
  putStrLn "Part2: real"
  print $ part2 $ lines inputReal