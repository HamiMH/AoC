import qualified  Data.Text as T
import qualified Data.Map.Strict as M
import qualified Data.Set as S

import Debug.Trace as D

partsToBagItem :: [T.Text] -> (String, Int)
partsToBagItem (num:st1:st2:xs)=(name,nmbr)
                        where nmbr=read$T.unpack num::Int
                              name=(T.unpack st1) ++ " " ++ (T.unpack st2)


bagContainsGoldBag :: M.Map String [(String, Int)]  -> String-> Bool
bagContainsGoldBag _ []=False
bagContainsGoldBag mapBag bag       | "shiny gold" `elem` strBags = True
                                    | otherwise                   = elem True  $ map (bagContainsGoldBag mapBag) $ strBags
                                          where strBags=map fst $ M.findWithDefault [("0",0)] (bag) mapBag

strContainmentToMap strCont   |strCont=="no other bags."    = []
                              |otherwise                    = createdMap
                              where createdMap= separItemBagItems
                                    textCont=T.pack strCont
                                    trimed= T.replace (T.pack ".") (T.pack "") textCont
                                    separItems = T.splitOn (T.pack ", ") trimed
                                    separItemWords=map T.words separItems
                                    separItemBagItems=map partsToBagItem $ separItemWords--import Debug.Trace (traceShow)  D.traceShowId--


convLineToTupple lline=(nameOfBag,containment)
                        where containment=strContainmentToMap $ T.unpack$head $ tail nameCont
                              textLine= T.pack lline
                              nameCont=T.splitOn (T.pack " bags contain ") textLine
                              nameOfBag= T.unpack$head nameCont



part1 input=show $ sum $ map boolToInt $ map (bagContainsGoldBag mapOfBags) (M.keys mapOfBags)
            where llines= lines input
                  mapOfBags = M.fromList $ map convLineToTupple llines

boolToInt :: Bool -> Int
boolToInt True= 1
boolToInt False= 0



resolveInnerOf ::M.Map String [(String, Int)] -> (String,Int) -> Int
resolveInnerOf mapOfBags   ("0",0)  =0
resolveInnerOf mapOfBags  (bagName,bagAmount)  = bagAmount + bagAmount*( sum $ map (resolveInnerOf mapOfBags) strBags )
                                                      --where strBags=D.traceShowId $ M.findWithDefault [("0",0)] bagName mapOfBags
                                                      where strBags= M.findWithDefault [("0",0)] bagName mapOfBags


part2 input= show $ (resolveInnerOf mapOfBags  ("shiny gold",1))-1
            where llines= lines input
                  mapOfBags = M.fromList $ map convLineToTupple llines


main = do

  inputTest <- readFile "inT"
  inputReal <- readFile "inR"
  putStrLn "Part1: test"
  print $ part1 $  inputTest
  putStrLn "Part1: real"
  print $ part1 $  inputReal

  putStrLn "Part2: test"
  print $ part2 $ inputTest
  putStrLn "Part2: real"
  print $ part2 $ inputReal