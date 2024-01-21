import qualified  Data.Text as T
import qualified Data.Map.Strict as M
import qualified Data.Set as S
import qualified Debug.Trace as D

neigbs=[    (-1,-1,-1),(-1,0,-1),(-1,1,-1),(0,-1,-1),(0,0,-1),(0,1,-1),(1,-1,-1),(1,0,-1),(-1,1,-1),
            (-1,-1, 0),(-1,0, 0),(-1,1, 0),(0,-1, 0),(0,0, 0),(0,1, 0),(1,-1, 0),(1,0, 0),(-1,1, 0),
            (-1,-1, 1),(-1,0, 1),(-1,1, 1),(0,-1, 1),(0,0, 1),(0,1, 1),(1,-1, 1),(1,0, 1),(-1,1, 1)   ]


parseLineToSet ::  Int -> Int -> S.Set (Int, Int, Int) -> [Char] -> S.Set (Int, Int, Int)
parseLineToSet _    _    accSet []        =accSet
parseLineToSet yPos xPos accSet (x:xs)    |x=='#'     = parseLineToSet yPos (xPos+1) (S.insert (xPos,yPos,0) accSet) xs
                                          |otherwise  = parseLineToSet yPos (xPos+1) ( accSet) xs

parseToSet ::  Int -> S.Set (Int, Int, Int) -> [[Char]] -> S.Set (Int, Int, Int)
parseToSet _ accSet []=accSet
parseToSet yPos accSet (x:xs)= parseToSet (yPos+1) (S.union accSet newSet) xs
                                    where newSet=parseLineToSet yPos 0 S.empty x



part1 :: [String] -> String
part1 input=show $ setInp
            where setInp =parseToSet 0 S.empty input
                  --neibInp = foldl S.union S.empty  parseToSet 0 S.empty input

            

tryMonad:: Monad m=> m Int -> m Int->m Int
tryMonad ma mb=do
                a<-ma
                b<-mb
                return $  a + b

tupAdd (a,b,c) (d,e,f)=(a+d,b+e,c+f)                
cross xs ys = S.map (\x -> S.map (\y ->(tupAdd x y)) ys) xs

part2 :: p -> String
part2 input= show $ cross (S.fromList [(10,10,10)]) (S.fromList neigbs)



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