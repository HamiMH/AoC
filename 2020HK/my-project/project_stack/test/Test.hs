 import System.Directory 


prLines i llines=
                  if (not.null) llines 
                    then                     
                        do
                            let str=show i
                            writeFile "out" str
                            let he=head llines
                            writeFile "out" (show he)
                            prLines (i+1) (tail llines)
                            return 0                           
                    else
                            return 0                         


main :: IO ()
main = do
        putStrLn "Hello World"
        let path =  getCurrentDirectory
        lline<- readFile "in"
        writeFile "out" ""
        let llines=lines lline
        let a = prLines 1 llines
        writeFile "out1" lline
        --appendFile "out" "\n"
        --appendFile "out" lline



