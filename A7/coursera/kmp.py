# python3
import sys

def compute_suffix(str):
    s=[0 for i in range(len(str))]
    counter=0
    for i in range(1,len(str)):
        while counter>0 and str[i]!=str[counter]:
            counter=s[counter-1]
        if str[i]==str[counter]:
            counter+=1
        s[i]=counter
    return s
def find_pattern(pattern, text):
  """
  Find all the occurrences of the pattern in the text
  and return a list of all positions in the text
  where the pattern starts in the text.
  """
  result=[]
  new_str=pattern+"$"+text
  s=compute_suffix(new_str)
  for i in range(len(pattern),len(s)):
      if s[i]>=len(pattern):
          result.append(i-2*len(pattern))
  return result


if __name__ == '__main__':
  pattern = sys.stdin.readline().strip()
  text = sys.stdin.readline().strip()
  result = find_pattern(pattern, text)
  print(" ".join(map(str, result)))

