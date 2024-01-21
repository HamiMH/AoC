import qualified  Data.Text as T
import qualified Data.Map.Strict as M
import qualified Data.Set as S
import qualified Debug.Trace as D

goSimulation1::[(String, Int)]->Int-> Int ->S.Set Int ->(Int,Bool)
goSimulation1 tupList index value attended      | S.member index attended     = (value,False)
                                                | length tupList ==index  = (value,True)
                                                | currType=="acc"             = goSimulation1 tupList (index+1) (value+currVal) newAttended
                                                | currType=="jmp"             = goSimulation1 tupList (index+currVal) value newAttended
                                                | currType=="nop"             = goSimulation1 tupList (index+1) value newAttended
                                                |otherwise                    = (-1,False)
                                                where (currType,currVal)=tupList !!index
                                                      newAttended=S.insert index attended

strsToTup :: [String] -> (String, Int)
strsToTup [x1, x2]=(x1,read nowX2)
                  where nowX2=if head x2=='+'
                        then tail x2
                        else x2

changeAndSimulate2 :: [(String, Int)] -> [(String, Int)] -> Int
changeAndSimulate2 _ []=(-1)
changeAndSimulate2 _ [x1]=(-1)
changeAndSimulate2 [] _=(-1)
changeAndSimulate2 xs (x1:xe)       | strAtIndex=="acc"           =nextIter
                                    | snd simulatedVal            =fst simulatedVal
                                    | otherwise                   = nextIter

                                          where itemAtIndex=x1
                                                (strAtIndex,intAtIndex)=itemAtIndex
                                                newParLi=if strAtIndex=="nop"
                                                            then xs ++ ("jmp",intAtIndex) : xe
                                                            else xs ++ ("nop",intAtIndex) : xe
                                                simulatedVal=goSimulation1 newParLi 0 0 S.empty
                                                nextIter=changeAndSimulate2 ((xs ++ [x1])) ( xe)

part1 :: [String] -> String
part1 input=show $ goSimulation1 partLi 0 0 S.empty
            where partLi =map  (strsToTup.words) input
            
part2 input= show $   changeAndSimulate2 ([head partLi]) ( (tail partLi) )
            where partLi =map  (strsToTup.words) input


main = do

  inputTest <- readFile "inT"
  inputReal <- readFile "inR"
  putStrLn "Part1: test"
  print $ part1 $ lines inputTest
  putStrLn "Part1: real"
  print $ part1 $  lines inputReal

  putStrLn "Part2: test"
  print $ part2 $ lines inputTest
  putStrLn "Part2: real"
  print $ part2 $ lines inputReal