var
  i, j, k, numberOfElementsInTriangle, numberOfLevels : integer;
  
begin
  readln(k);
  
  numberOfLevels := 0;
  
  while numberOfLevels * (numberOfLevels + 1) div 2 < k do
    numberOfLevels += 1;
    
  numberOfLevels -= 1;
  
  numberOfElementsInTriangle := numberOfLevels * (numberOfLevels + 1) div 2;
  
  j := k - numberOfElementsInTriangle;
  i := j - numberOfLevels - 2;
  
  writeln('i = ', i);
  writeln('j = ', j);

end.