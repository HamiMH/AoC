
import Control.Monad.Reader

getFirst=do
    name<-ask
    return (name ++ " woke up")

getSecond=do
    name<-ask
    return (name ++ " wrote some Haskell")

getStory=do
    first <-getFirst
    second<- getSecond
    return ("First, "++first ++ ". Second, "++second)

main = do
   putStrLn $ runReader getStory "Nekdo"
