import Data.Array
import qualified Data.Text as T
import qualified Data.Map.Strict as M
import qualified Data.Set as S
import qualified Debug.Trace as D
import qualified Data.Time as Ti
import qualified Data.List as Li
import qualified Data.List.Split as LiSp
import qualified Data.Strings  as Str
import qualified Data.ByteString.Internal  as BS
import Control.Monad (join)

data Rule = Rule String Int Int Int Int deriving(Eq,Show)


valIsValidByRule value (Rule str a b c d)   | (value>=a) &&  (value <=b)    = True
                                        | (value>=c) &&  (value <=d)    = True
                                        | otherwise                     = False


valIsValidByRules rules value  = any (==True) $ map (valIsValidByRule value) rules

parseLineToRules lline =Rule (head splitedLLine) a b c d
                        where   splitedLLine =LiSp.splitOn ":" lline
                                impPart=head $ tail $ splitedLLine
                                impPartTrim=Str.strTrim impPart
                                sidesOfOr=LiSp.splitOn " or " impPartTrim
                                sidesOfMinus=map (LiSp.splitOn "-") sidesOfOr
                                [[a,b],[c,d]]=(map . map) read sidesOfMinus ::[[Int]]

strLineToIntList strLine=    map read $ LiSp.splitOn "," strLine ::[Int]

part1 input=show $ sum filtered
              where parts=LiSp.splitOn "\n\n" input
                    listRules=map parseLineToRules (lines $ head parts)

                    strPartMyTicket= head $ tail (lines $ head $ tail parts)
                    strPartNearTickets=  tail (lines $ head $ tail $ tail parts)

                    listMyTickets=strLineToIntList strPartMyTicket
                    listNearTickets=  map strLineToIntList strPartNearTickets

                    joined=join listNearTickets
                    filtered=filter (not . (valIsValidByRules listRules)) joined



interme listRules = valIsValidByRules listRules

intermeMap listRules= map (map (interme listRules))


--fceRules listNearTickets listRules = map ( map (valIsValidByRules listRules)) listNearTickets
--fceRules listNearTickets listRules = map (all (==True)) $ map ( map (valIsValidByRules listRules)) listNearTickets

part2 input =show $   listValidTickets
                where   parts=LiSp.splitOn "\n\n" input
                        listRules=map parseLineToRules (lines $ head parts)

                        strPartMyTicket= head $ tail (lines $ head $ tail parts)
                        strPartNearTickets=  tail (lines $ head $ tail $ tail parts)

                        listMyTickets=strLineToIntList strPartMyTicket
                        listNearTickets =  map strLineToIntList strPartNearTickets
                        listValidTickets = filter (validateValuesAll ) listNearTickets

                        validateValue=valIsValidByRules listRules
                        validateValues=map validateValue
                        validateValuesAll=all id . validateValues

main = do

  inputTest <- readFile "inT"
  inputReal <- readFile "inR"

  startTimeT1 <- Ti.getCurrentTime
  putStrLn "Part1: test"
  print $ part1  (inputTest)
  endTimeT1 <- Ti.getCurrentTime
  print $ show $ Ti.diffUTCTime endTimeT1 startTimeT1
  print $ "---------------------------------------"

  startTimeR1 <- Ti.getCurrentTime
  putStrLn "Part1: real"
  print $ part1 ( inputReal)
  endTimeR1 <- Ti.getCurrentTime
  print $ show $ Ti.diffUTCTime endTimeR1 startTimeR1
  print $ "#######################################"

  startTimeT2 <- Ti.getCurrentTime
  putStrLn "Part2: test"
  print $ part2  ( inputTest)
  endTimeT2 <- Ti.getCurrentTime
  print $ show $ Ti.diffUTCTime endTimeT2 startTimeT2
  print $ "---------------------------------------"

  startTimeR2 <- Ti.getCurrentTime
  putStrLn "Part2: real"
  print $ part2  ( inputReal)
  endTimeR2 <- Ti.getCurrentTime
  print $ show $ Ti.diffUTCTime endTimeR2 startTimeR2
  print $ "#######################################"
  print $ "Total time:"
  print $ show $ Ti.diffUTCTime endTimeR2 startTimeT1

