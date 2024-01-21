import qualified  Data.Text as T
import Data.Char(isSpace)


data Record = Record Int Int Char String
  deriving Show

parse :: String -> Record
parse inStr = Record minCount maxCount charPolicy password
  where
    line       = T.pack inStr
    parts      = T.words line
    counts     = T.splitOn (T.pack "-") (head parts)
    minCount   = read . T.unpack . head $ counts :: Int
    maxCount   = read . T.unpack . last $ counts :: Int
    charPolicy = head . T.unpack $ parts !! 1 :: Char
    password   = dropWhile isSpace $ show$T.strip$last parts



isOk1::Record->Int
isOk1 (Record minCount maxCount charPolicy password)    |   nOfChars>=minCount && nOfChars<=maxCount  = 1
                                                        |   otherwise                                  = 0
                                                    where nOfChars=charOccure password charPolicy

charOccure :: String -> Char -> Int
charOccure [] _ = 0
charOccure (x:xs) ch    | x==ch = 1+rest
                        | otherwise = 0+rest
                        where rest = charOccure xs ch

boolToInt boo|boo=1
            |otherwise =0

isOk2::Record->Int
isOk2 (Record minCount maxCount charPolicy password)    |   boolToInt (minChar==charPolicy) + boolToInt ( maxChar==charPolicy) ==1  = 1
                                                        |   otherwise                                  = 0

                                                        where   minChar=password!!(minCount)
                                                                maxChar=password!!(maxCount)

part1 llines=
    sum (map (isOk1.parse) llines)

part2 llines=
    sum (map (isOk2.parse) llines)

main = do

  inputTest <- readFile "inT"
  inputReal <- readFile "inR"
  let aa="sadas fsdss sadas"
  let line       = T.pack aa
  let parts      = T.words line
  let counts     = T.splitOn (T.pack "-") (head parts)
  let  minCount   = read . T.unpack . head $ counts :: Int
  let  maxCount   = read . T.unpack . last $ counts :: Int
  let  charPolicy = head . T.unpack $ parts !! 1 :: Char
  let  password   = dropWhile isSpace $ show$last parts

  putStrLn $show parts
  putStrLn password
  putStrLn $ show (take 1 password)
  let  password2   = "fgfddsf"

  putStrLn password2
  putStrLn $ show (take 1 password2)
  putStrLn "Part1: test"
  print $ part1 $ lines inputTest
  putStrLn "Part1: real"
  print $ part1 $ lines inputReal

  putStrLn "Part2: test"
  print $ part2 $ lines inputTest
  putStrLn "Part2: real"
  print $ part2 $ lines inputReal