import qualified  Data.Text   as T
import qualified  Data.Set    as S

inputToListListSet input=itemsMap
                    where itemsStr= map T.unpack $ T.splitOn (T.pack "\n\n") (T.pack input)
                          itemsStr2= map lines itemsStr
                          --itemsMap3= map  resolvePack itemsStr2
                          itemsMap= (map . map) S.fromList itemsStr2

part1 input=show $ sum itemsSizes
          where itemsMap= inputToListListSet input
                itemsMap2= map (foldl S.union S.empty) itemsMap
                itemsSizes=map S.size itemsMap2

part2 input= show $ sum itemsSizes
          where itemsMap= inputToListListSet input
                itemsMap2= map (foldl S.intersection $S.fromList "abcdefghijklmnopqrstuvwxyz") itemsMap
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