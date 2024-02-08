import qualified  Data.Text as T
import qualified Data.Map.Strict as M
import qualified Data.Set as S
import qualified Debug.Trace as D
import qualified Data.Time as Ti

findSumNthOfVal ::  Int -> Int -> [Int] -> Bool
findSumNthOfVal 0 0 _ = True
findSumNthOfVal _ 0 _ = False
findSumNthOfVal 0 _ _ = False
findSumNthOfVal _ _ [] = False

findSumNthOfVal targetSum rem (x1:xs)   | findSumNthOfVal (targetSum-x1) (rem-1) (xs)           = True
                                        |otherwise                                              = findSumNthOfVal targetSum rem (xs)


findInvalid :: [Int] -> [Int] -> Int
findInvalid (h:hs) (t:ts)               |found          = findInvalid (hs++[t]) ts
                                        |otherwise      = t
                                        where found     = findSumNthOfVal t 2 (h:hs)


evalPart1 input nOfPre= findInvalid firstPart rest
                where   numbInput=map read input ::[Int]
                        firstPart=take nOfPre numbInput--D.traceShowId$
                        rest=drop nOfPre numbInput

part1 input nOfPre=show $  evalPart1 input nOfPre


findTargetSum (h:hs) (t:ts) currSum totalSum    |currSum<totalSum       =findTargetSum ((h:hs)++[t]) (ts) (currSum+t) totalSum
                                                |currSum>totalSum       =findTargetSum hs (t:ts) (currSum-h) totalSum
                                                |otherwise              =(h:hs)

part2 input nOfPre=show $      (maximum getTargetList)+(minimum getTargetList)
                        where   targetVal=evalPart1 input nOfPre
                                (x:xs)=map read input ::[Int]
                                getTargetList= findTargetSum [x] xs x targetVal

main = do

  inputTest <- readFile "inT"
  inputReal <- readFile "inR"

  startTimeT1 <- Ti.getCurrentTime
  putStrLn "Part1: test"
  print $ part1  (lines inputTest) 5
  endTimeT1 <- Ti.getCurrentTime
  print $ show $ Ti.diffUTCTime endTimeT1 startTimeT1
  print $ "---------------------------------------"

  startTimeR1 <- Ti.getCurrentTime
  putStrLn "Part1: real"
  print $ part1 ( lines inputReal) 25
  endTimeR1 <- Ti.getCurrentTime
  print $ show $ Ti.diffUTCTime endTimeR1 startTimeR1
  print $ "#######################################"

  startTimeT2 <- Ti.getCurrentTime
  putStrLn "Part2: test"
  print $ part2  (lines inputTest) 5
  endTimeT2 <- Ti.getCurrentTime
  print $ show $ Ti.diffUTCTime endTimeT2 startTimeT2
  print $ "---------------------------------------"

  startTimeR2 <- Ti.getCurrentTime
  putStrLn "Part2: real"
  print $ part2  (lines inputReal) 25
  endTimeR2 <- Ti.getCurrentTime
  print $ show $ Ti.diffUTCTime endTimeR2 startTimeR2
  print $ "#######################################"
  print $ "Total time:"
  print $ show $ Ti.diffUTCTime endTimeR2 startTimeT1

