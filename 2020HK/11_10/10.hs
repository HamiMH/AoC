import Data.Array
import qualified Data.Text as T
import qualified Data.Map.Strict as M
import qualified Data.Set as S
import qualified Debug.Trace as D
import qualified Data.Time as Ti
import qualified Data.List as Li
--import qualified Data.Array as Arr

--import qualified Data.Function.Memoize as Memo
--import qualified Data.MemoCombinators as Memo
--import qualified GHC.Arr as Memo


findSkips [] currVolt (skips1,skips2,skips3)  =                 (skips1,skips2,skips3+1)
findSkips (x:xs) currVolt (skips1,skips2,skips3)        |(x-currVolt)==1        =findSkips (xs) x (skips1+1,skips2,skips3)
                                                        |(x-currVolt)==2        =findSkips (xs) x (skips1,skips2+1,skips3)
                                                        |(x-currVolt)==3        =findSkips (xs) x (skips1,skips2,skips3+1)
                                                        |otherwise              =                 (skips1,skips2,skips3+1)

evalPart1 input = findSkips sortedInput 0 (0,0,0)
                where   numbInput=map read input ::[Int]
                        sortedInput= Li.sort numbInput

part1 input=show $  (skips1)*(skips3)
             where   (skips1,skips2,skips3) =evalPart1 input


numberOfCombArr listInp = fce
  where arrInp     = listArray (0, listInpLen -1) listInp
        listInpLen=length listInp

        fce currVolt index  | currArrVal -currVolt >3               = 0
                            | index    == listInpLen-1              = 1
                            | otherwise                             = fceArr ! (currVolt, index+1) + fceArr ! ( currArrVal,index+1) --D.traceShowId$
                            where currArrVal = arrInp ! index

        fceArr = listArray bounds
               [fce i j | (i, j) <- range bounds]
        bounds = ((0, 0), ( last listInp, listInpLen))



part2 input =show $   numberOfCombArr sortedInput 0 0 -- 
                 -- numberOfComb sortedInput 0 0
                 where  numbInput=map read input ::[Int]
                        sortedInput= Li.sort numbInput

main = do

  inputTest <- readFile "inT"
  inputReal <- readFile "inR"

  startTimeT1 <- Ti.getCurrentTime
  putStrLn "Part1: test"
  print $ part1  (lines inputTest)
  endTimeT1 <- Ti.getCurrentTime
  print $ show $ Ti.diffUTCTime endTimeT1 startTimeT1
  print $ "---------------------------------------"

  startTimeR1 <- Ti.getCurrentTime
  putStrLn "Part1: real"
  print $ part1 ( lines inputReal)
  endTimeR1 <- Ti.getCurrentTime
  print $ show $ Ti.diffUTCTime endTimeR1 startTimeR1
  print $ "#######################################"

  startTimeT2 <- Ti.getCurrentTime
  putStrLn "Part2: test"
  print $ part2  (lines inputTest)
  endTimeT2 <- Ti.getCurrentTime
  print $ show $ Ti.diffUTCTime endTimeT2 startTimeT2
  print $ "---------------------------------------"

  startTimeR2 <- Ti.getCurrentTime
  putStrLn "Part2: real"
  print $ part2  (lines inputReal)
  endTimeR2 <- Ti.getCurrentTime
  print $ show $ Ti.diffUTCTime endTimeR2 startTimeR2
  print $ "#######################################"
  print $ "Total time:"
  print $ show $ Ti.diffUTCTime endTimeR2 startTimeT1

