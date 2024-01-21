import qualified  Data.Text as T
import qualified Data.Map.Strict as M
import Text.Read (readMaybe)



wordToTuple word= (a,b)
          where parts=T.splitOn (T.pack ":") word 
                a=head parts
                b=last parts

parseToPass :: T.Text -> [(T.Text, T.Text)]
parseToPass line =map wordToTuple (T.splitOn (T.pack " ") line )

mapParseToPass itemsStr=map parseToPass itemsStr


resolveHgt inText | T.isInfixOf (T.pack "cm") inText = evalCm
                  | T.isInfixOf (T.pack "in") inText = evalIn
                  | otherwise = False
                  where evalCm= heiCm>=150 && heiCm<=193
                        heiCm=read$ T.unpack $ T.replace (T.pack "cm") (T.pack "") inText::Int
                        evalIn= heiIn>=59 && heiIn<=76
                        heiIn=read$ T.unpack $ T.replace (T.pack "in") (T.pack "") inText::Int

passIsCorrect :: M.Map T.Text T.Text -> Int
passIsCorrect mmap  | sizeCheck mmap==0 =  0
 --                   | byr>=1920 && byr <=2002 && iyr>=2010 && iyr <=2020 && eyr>=2020 && eyr<=2030 && ( ( hgtCm>=150 && hgtCm<=193 ) ||hgtCm==(-1) )&& ( ( hgtIn>=59 && hgtIn<=76 ) ||hgtIn==(-1) ) && thisHclCorrect&& thisEclCorrect && thisPidCorrect = 1
                    | byr>=1920 && byr <=2002 && iyr>=2010 && iyr <=2020 && eyr>=2020 && eyr<=2030 && (resolveHgt hgtStr) && thisHclCorrect&& thisEclCorrect && thisPidCorrect = 1
                     |otherwise=0
                        where   byr= read $ T.unpack $ M.findWithDefault (T.pack "0") (T.pack "byr") mmap :: Int
                                iyr= read$ T.unpack $ M.findWithDefault (T.pack "0") (T.pack "iyr") mmap ::Int
                                eyr= read$ T.unpack $ M.findWithDefault (T.pack "0") (T.pack "eyr") mmap ::Int
                                hgtStr= M.findWithDefault (T.pack "0") (T.pack "hgt")  mmap
                                hclStr= M.findWithDefault (T.pack "0") (T.pack "hcl") mmap
                                eclStr= M.findWithDefault (T.pack "0") (T.pack "ecl") mmap
                                eclRealStr=T.unpack eclStr
                                pidStr= M.findWithDefault (T.pack "0") (T.pack "pid") mmap
                                
                                --hgtCm=if T.isInfixOf (T.pack "cm") hgtStr
                                --        then read$ T.unpack $ T.replace (T.pack "cm") (T.pack "") hgtStr::Int
                                --        else (-1)
                                --hgtIn=if T.isInfixOf (T.pack "in") hgtStr
                                --        then read$ T.unpack $ T.replace (T.pack "in") (T.pack "") hgtStr::Int
                                --        else (-1)
                                thisHclCorrect=(T.length hclStr ==7 ) && (T.head hclStr=='#')&& (checkRestHair $ tail $ T.unpack hclStr)
                                thisEclCorrect= (eclRealStr=="amb") ||(eclRealStr==  "blu") ||(eclRealStr==  "brn") ||(eclRealStr==  "gry") ||(eclRealStr==  "grn") ||(eclRealStr==  "hzl") ||(eclRealStr==  "oth")
                                thisPidCorrect= (length (T.unpack pidStr)==9 )&& (checkPid $ T.unpack pidStr)


checkPid []= True
checkPid (x:xs)| (x<='9' && x>='0') = checkRestHair xs 
                    | otherwise =False

checkRestHair :: [Char] -> Bool
checkRestHair []= True
checkRestHair (x:xs)| (x<='9' && x>='0') || (x<='f' && x>='a') = checkRestHair xs
                    | otherwise =False


sizeCheck mmap  | M.size mmap >=7   =1
                | otherwise           =0

part1 input=show $ sum okSizes
          where itemsStr= T.splitOn (T.pack "\n\n") (T.pack input) 
                itemsStr2= map ( T.replace (T.pack "\n") (T.pack " ")) itemsStr 
                items= mapParseToPass itemsStr2
                itemsMap=map M.fromList items
                itemsMapModif= map ( M.delete (T.pack"cid")) itemsMap
                okSizes=map sizeCheck itemsMapModif



part2 input= show $ sum okSizes
          where itemsStr= T.splitOn (T.pack "\n\n") (T.pack input) 
                itemsStr2= map ( T.replace (T.pack "\n") (T.pack " ")) itemsStr 
                items= mapParseToPass itemsStr2
                itemsMap=map M.fromList items
                itemsMapModif= map ( M.delete (T.pack"cid")) itemsMap
                okSizes=map passIsCorrect itemsMapModif

main = do

  inputTest <- readFile "inT1"
  inputReal <- readFile "inR"
  putStrLn "Part1: test"
  print $ part1 $  inputTest
  putStrLn "Part1: real"
  print $ part1 $  inputReal

  putStrLn "Part2: test"
  print $ part2 $ inputTest
  putStrLn "Part2: real"
  print $ part2 $ inputReal