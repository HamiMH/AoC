
import Control.Monad.Reader
import Data.Binary.Builder (singleton)
import qualified  Data.Text as T
import qualified Data.Map.Strict as M
import qualified Data.Set as S
import qualified Debug.Trace as D


--cross xs ys = S.map (\x -> S.map (\y ->(tupAdd x y)) ys) xs
cross ::S.Set Int -> S.Set Int -> S.Set Int
cross xs ys = S.unions $ S.map (\x -> S.map (\y ->(x+ y)) ys) xs


main = do
   putStrLn $  S.showTree $ cross  (S.fromList [2,4,6])(S.fromList  [ 20,40,60])
   putStrLn $  show $ S.member 2  $ cross  (S.fromList [2,4,6])(S.fromList  [ 20,40,60])