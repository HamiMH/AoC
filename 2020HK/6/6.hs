import qualified  Data.Text as T
import qualified Data.Map.Strict as M
import qualified Data.Set as S
import Text.Read (readMaybe)
import Data.IntMap (union)



reduceWholeList ::[S.Set Char]->S.Set Char->(S.Set Char->S.Set Char->S.Set Char)->  S.Set Char
reduceWholeList [] inSet _=inSet
reduceWholeList (x:xs) inSet oper= reduceWholeList xs (oper x inSet) oper

runReduce :: [S.Set Char] -> S.Set Char
--runReduce inMaps= reduceWholeList inMaps S.empty S.union
runReduce inMaps= foldl S.union S.empty inMaps  
runReduce2 inMaps= reduceWholeList inMaps (S.fromList "abcdefghijklmnopqrstuvwxyz") S.intersection
runReduce2 inMaps= foldl S.union S.empty inMaps  

resolvePack :: [String] -> [S.Set Char]
resolvePack inStrings= map S.fromList inStrings



inputToListListSet input=itemsMap
                    where itemsStr= map T.unpack $ T.splitOn (T.pack "\n\n") (T.pack input) 
                          itemsStr2= map lines itemsStr 
                          itemsMap= map  resolvePack itemsStr2 

part1 input=show $ sum itemsSizes
          where itemsMap= inputToListListSet input 
                itemsMap2= map runReduce itemsMap
                itemsSizes=map S.size itemsMap2




part2 input= show $ sum itemsSizes
          where itemsMap= inputToListListSet input 
                itemsMap2= map runReduce2 itemsMap
                itemsSizes=map S.size itemsMap2

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