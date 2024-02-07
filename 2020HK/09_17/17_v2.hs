import qualified  Data.Text as T
import qualified Data.Map.Strict as M
import qualified Data.Set as S
import qualified Debug.Trace as D
import qualified Data.Time as Ti

data MyPoint1 = MyPoint1 Int deriving(Eq,Show)
data MyPoint2 = MyPoint2 Int Int deriving(Eq,Show,Ord)
data MyPoint3 = MyPoint3 Int Int Int deriving(Eq,Show,Ord)
data MyPoint4 = MyPoint4 Int Int Int Int deriving(Eq,Show,Ord)


instance Num MyPoint3 where     (MyPoint3 a1 a2 a3)+(MyPoint3 b1 b2 b3)=MyPoint3 (a1+b1) (a2+b2) (a3+b3)
                                (MyPoint3 a1 a2 a3)-(MyPoint3 b1 b2 b3)=MyPoint3 (a1-b1) (a2-b2) (a3-b3) 
instance Num MyPoint4 where     (MyPoint4 a1 a2 a3 a4)+(MyPoint4 b1 b2 b3 b4)=MyPoint4 (a1+b1) (a2+b2) (a3+b3) (a4+b4)
                                (MyPoint4 a1 a2 a3 a4)-(MyPoint4 b1 b2 b3 b4)=MyPoint4 (a1-b1) (a2-b2) (a3-b3) (a4-b4)

pointExtend1 ext (MyPoint1 a )=MyPoint2 a ext
pointExtend2 ext (MyPoint2 a b)=MyPoint3 a b ext
pointExtend3 ext (MyPoint3 a b c)=MyPoint4 a b c ext

neigbs1=[MyPoint1 (-1),MyPoint1 0,MyPoint1 1]
neigbs2=concat[map (pointExtend1 (-1)) neigbs1,map (pointExtend1 (0)) neigbs1,map (pointExtend1 (1)) neigbs1]
neigbs3=concat[map (pointExtend2 (-1)) neigbs2,map (pointExtend2 (0)) neigbs2,map (pointExtend2 (1)) neigbs2]
neigbs4=concat[map (pointExtend3 (-1)) neigbs3,map (pointExtend3 (0)) neigbs3,map (pointExtend3 (1)) neigbs3]

neigbs3Set=S.fromList $! map Just neigbs3
neigbs4Set=S.fromList $! map Just neigbs4


parseLineToSet ::  Int -> Int -> S.Set MyPoint2 -> [Char] -> S.Set MyPoint2
parseLineToSet _    _    accSet []        =accSet
parseLineToSet yPos xPos accSet (x:xs)    |x=='#'     = parseLineToSet yPos (xPos+1) (S.insert (MyPoint2 xPos yPos) accSet) xs
                                          |otherwise  = parseLineToSet yPos (xPos+1) ( accSet) xs

parseToSet ::  Int -> S.Set MyPoint2 -> [[Char]] -> S.Set MyPoint2
parseToSet _ accSet []=accSet
parseToSet yPos accSet (x:xs)= parseToSet (yPos+1) (S.union accSet newSet) xs
                                    where newSet=parseLineToSet yPos 0 S.empty x


pointTurnOn collOfPnts neigbsSetMonad pntToCalc  | (S.member pntToCalc collOfPnts) && (numOfInters==3 || numOfInters==4)     =pntToCalc
                                                | (S.member pntToCalc collOfPnts)==False && numOfInters==3                  =pntToCalc
                                                | otherwise                                                                  =Nothing
        where neib=crossMonad (S.singleton pntToCalc) neigbsSetMonad
              inters=S.intersection neib collOfPnts
              numOfInters=S.size inters





runNcycles :: (Num t, Num a, Ord a, Eq t) => t -> S.Set (Maybe a) -> S.Set (Maybe a) -> S.Set (Maybe a)
runNcycles 0 _ activePnts=activePnts

runNcycles nOfCycles neigbsSetMonad activePnts   =runNcycles (nOfCycles-1) neigbsSetMonad turnedAfterOneIter 
                                                where
                                                    neibInp = crossMonad neigbsSetMonad activePnts
                                                    fcePoinTurnOnWithInp=pointTurnOn activePnts neigbsSetMonad
                                                    turnedAfterOneIter=S.map fcePoinTurnOnWithInp neibInp 

sizeOfJusts sset  |S.member Nothing sset  = S.size sset -1
                  |otherwise              = S.size sset

part1 :: [String] -> String
part1 input=show $      sizeOfJusts turnedAtEnd --turnedAfterOneIter
                where   setInp =parseToSet 0 S.empty input --D.traceShowId$
                        set3d=S.map (pointExtend2 0) setInp
                        setInpMon=S.map Just set3d
                        turnedAtEnd=runNcycles 6 neigbs3Set setInpMon 
                  


part2 :: [String] -> String
part2 input=show $      sizeOfJusts turnedAtEnd --turnedAfterOneIter
                where   setInp =parseToSet 0 S.empty input --D.traceShowId$
                        set3d=S.map (pointExtend3 0)$ S.map (pointExtend2 0) setInp
                        setInpMon=S.map Just set3d
                        turnedAtEnd=runNcycles 6 neigbs4Set setInpMon 

            

tupAddMonad :: (Monad m, Num a) => m a -> m a -> m a
tupAddMonad ma mb=do
                a<-ma
                b<-mb
                return $! a+ b


crossMonad xs ys = S.unions $! S.map (\x -> S.map (\y ->(tupAddMonad x y)) ys) xs




main = do

  inputTest <- readFile "inT"
  inputReal <- readFile "inR"

  startTimeT1 <- Ti.getCurrentTime
  putStrLn "Part1: test"
  print $ part1 $ lines inputTest
  endTimeT1 <- Ti.getCurrentTime
  print $ show $ Ti.diffUTCTime endTimeT1 startTimeT1
  print $ ""

  startTimeR1 <- Ti.getCurrentTime
  putStrLn "Part1: real"
  print $ part1 $  lines inputReal
  endTimeR1 <- Ti.getCurrentTime
  print $ show $ Ti.diffUTCTime endTimeR1 startTimeR1
  print $ ""

  startTimeT2 <- Ti.getCurrentTime
  putStrLn "Part2: test"
  print $ part2 $ lines inputTest
  endTimeT2 <- Ti.getCurrentTime
  print $ show $ Ti.diffUTCTime endTimeT2 startTimeT2
  print $ ""

  startTimeR2 <- Ti.getCurrentTime
  putStrLn "Part2: real"
  print $ part2 $ lines inputReal
  endTimeR2 <- Ti.getCurrentTime
  print $ show $ Ti.diffUTCTime endTimeR2 startTimeR2
  print $ ""
  print $ "Total time:"
  print $ show $ Ti.diffUTCTime endTimeR2 startTimeT1

