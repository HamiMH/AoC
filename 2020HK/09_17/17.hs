import qualified  Data.Text as T
import qualified Data.Map.Strict as M
import qualified Data.Set as S
import qualified Debug.Trace as D
import qualified Control.Monad.Cont as S
import GHC.Data.SizedSeq (ssElts)


neigbs=[    (-1,-1,-1),(-1,-1,0),(-1,-1,1),(-1,0,-1),(-1,0,0),(-1,0,1),(-1,1,-1),(-1,1,0),(-1,1,1),
            ( 0,-1,-1),( 0,-1,0),( 0,-1,1),( 0,0,-1),( 0,0,0),( 0,0,1),( 0,1,-1),( 0,1,0),( 0,1,1),
            ( 1,-1,-1),( 1,-1,0),( 1,-1,1),( 1,0,-1),( 1,0,0),( 1,0,1),( 1,1,-1),( 1,1,0),( 1,1,1)  ]
neigbsSet=S.fromList neigbs
neigbsSetMonad=S.map Just (S.fromList neigbs)


neigbsSetMonad4 =S.unions [S.map (tupExtendMonad (Just (-1))) neigbsSetMonad,S.map (tupExtendMonad (Just (0))) neigbsSetMonad,S.map (tupExtendMonad (Just (1))) neigbsSetMonad ]

parseLineToSet ::  Int -> Int -> S.Set (Int, Int, Int) -> [Char] -> S.Set (Int, Int, Int)
parseLineToSet _    _    accSet []        =accSet
parseLineToSet yPos xPos accSet (x:xs)    |x=='#'     = parseLineToSet yPos (xPos+1) (S.insert (xPos,yPos,0) accSet) xs
                                          |otherwise  = parseLineToSet yPos (xPos+1) ( accSet) xs

parseToSet ::  Int -> S.Set (Int, Int, Int) -> [[Char]] -> S.Set (Int, Int, Int)
parseToSet _ accSet []=accSet
parseToSet yPos accSet (x:xs)= parseToSet (yPos+1) (S.union accSet newSet) xs
                                    where newSet=parseLineToSet yPos 0 S.empty x


pointTurnOn collOfPnts pntToCalc  | (S.member pntToCalc collOfPnts) && (numOfInters==3 || numOfInters==4)   =pntToCalc
                                  | (S.member pntToCalc collOfPnts)==False && numOfInters==3                =pntToCalc
                                  |otherwise                        =Nothing
        where neib=crossMonad (S.singleton pntToCalc) neigbsSetMonad
              inters=S.intersection neib collOfPnts
              numOfInters=S.size inters


pointTurnOn4 collOfPnts pntToCalc   | (S.member pntToCalc collOfPnts) && (numOfInters==3 || numOfInters==4)   =pntToCalc
                                    | (S.member pntToCalc collOfPnts)==False && numOfInters==3                =pntToCalc
                                    |otherwise                        =Nothing
        where neib=crossMonad4 (S.singleton pntToCalc) neigbsSetMonad4
              inters=S.intersection neib collOfPnts
              numOfInters=S.size inters



runNcycles :: Int -> S.Set (Maybe (Int, Int, Int))-> S.Set (Maybe (Int, Int, Int))
runNcycles 0 activePnts=activePnts
runNcycles nOfCycles activePnts=runNcycles (nOfCycles-1) turnedAfterOneIter
                              where
                              neibInp = crossMonad neigbsSetMonad activePnts
                              fcePoinTurnOnWithInp=pointTurnOn activePnts
                              turnedAfterOneIter=S.map fcePoinTurnOnWithInp neibInp

runNcycles4 :: Int -> S.Set (Maybe (Int, Int, Int, Int))-> S.Set (Maybe (Int, Int, Int, Int))
runNcycles4 0 activePnts=activePnts
runNcycles4 nOfCycles activePnts=runNcycles4 (nOfCycles-1) turnedAfterOneIter
                              where
                              neibInp = crossMonad4 neigbsSetMonad4 activePnts
                              fcePoinTurnOnWithInp=pointTurnOn4 activePnts
                              turnedAfterOneIter=S.map fcePoinTurnOnWithInp neibInp

sizeOfJusts sset  |S.member Nothing sset  = S.size sset -1
                  |otherwise              = S.size sset

part1 :: [String] -> String
part1 input=show $ sizeOfJusts turnedAtEnd --turnedAfterOneIter
            where setInp =parseToSet 0 S.empty input --D.traceShowId$
                  setInpMon=S.map Just setInp
                  --neibInp = crossMonad neigbsSetMonad setInpMon
                  --fcePoinTurnOnWithInp=pointTurnOn setInpMon
                  --turnedAfterOneIter=S.map fcePoinTurnOnWithInp neibInp
                  turnedAtEnd=runNcycles 6 setInpMon
                  


part2 :: [String] -> String
part2 input= show $ sizeOfJusts turnedAtEnd
            where
                  setInp =parseToSet 0 S.empty input
                  setInpMon=S.map Just setInp
                  tupExtendMonad0=tupExtendMonad (Just 0)
                  setInpMonExt=S.map tupExtendMonad0 setInpMon
                  --fcePoinTurnOnWithInp=pointTurnOn4 setInpMonExt
                  turnedAtEnd=runNcycles4 6 setInpMonExt

            

--tupAddMonad :: (Num a, Num b, Num c) => Maybe (a, b, c) -> Maybe (a, b, c) -> Maybe (a, b, c)
tupAddMonad :: (Monad m, Num a) => m (a, a, a) -> m (a, a, a) -> m (a, a, a)
tupAddMonad ma mb=do
                a<-ma
                b<-mb
                return $ tupAdd a b
tupAddMonad4 :: (Monad m, Num a) => m (a, a, a, a) -> m (a, a, a, a) -> m (a, a, a, a)
tupAddMonad4 ma mb=do
                a<-ma
                b<-mb
                return $ tupAdd4 a b

tupExtendMonad :: (Monad m, Num a) => m a ->m (a, a, a) ->  m (a, a, a,a)
tupExtendMonad md ma =do
                a<-ma
                d<-md
                return $ tupExtend d a


--tupAdd :: (Num a) => (a, a, a) -> (a, a, a) -> (a, a, a)
--tupAdd :: (Num a) => (a, a, a, a) -> (a, a, a, a) -> (a, a, a, a)
tupAdd (a,b,c) (d,e,f)=(a+d,b+e,c+f)        
tupAdd4 (a,b,c,d) (e,f,g,h)=(a+e,b+f,c+g,d+h)        


tupExtend :: d -> (a, b, c) -> (a, b, c, d)
tupExtend ex (a,b,c)=(a,b,c,ex)

cross xs ys = S.unions $ S.map (\x -> S.map (\y ->(tupAdd x y)) ys) xs
crossMonad xs ys = S.unions $ S.map (\x -> S.map (\y ->(tupAddMonad x y)) ys) xs
crossMonad4 xs ys = S.unions $ S.map (\x -> S.map (\y ->(tupAddMonad4 x y)) ys) xs




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
