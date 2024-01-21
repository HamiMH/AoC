
import Control.Monad.Reader
import Data.Binary.Builder (singleton)



tryMonad:: Monad m=> (Int->Int->Int) -> m Int -> m Int->m Int
tryMonad f ma mb=do
                a<-ma
                b<-mb
                return $ f a b

main = do
   putStrLn $  show $ tryMonad (+) (Nothing) (Nothing) 
   putStrLn $  show $ tryMonad (+) (Just 2) (Nothing) 
   putStrLn $  show $ tryMonad (+) (Nothing) (Just 3) 
   putStrLn $  show $ tryMonad (+) (Just 2) (Just 25) 

   putStrLn $  show $ tryMonad (+) ( [2]) ([ 25]) 
   putStrLn $  show $ tryMonad (+) ( [0]) ([ 0]) 
   putStrLn $  show $ tryMonad (+) ( []) ([ 25]) 
   putStrLn $  show $ tryMonad (+) ( [2]) ([ 0]) 
   putStrLn $  show $ tryMonad (+) ( []) ([ ]) 
   putStrLn $  show $ tryMonad (+) ( [2,2]) ([ 0,1]) 
   putStrLn $  show $ tryMonad (+) ( [1,2]) ([10,20]) 
