# python3
import sys

class Node:
    def __init__(self,suffix,txt):
        self.suffix=suffix
        self.txt=txt
    def __lt__(self,other):
        return self.txt<other.txt
    def __eq__(self,other):
        return self.txt==other.txt

def f1(text):
    matrix=[]
    

    i=0
    new_text=""
    while i<len(text):
        new_text=text[-1]
        new_text+=text[0:len(text)-1]
        text=new_text
        n=Node(len(text)-i-1,text)
        matrix.append(n)
        i+=1
    # for i in range(len(matrix)):
    #     for j in range(i+1,len(matrix)):
    #         for k in range(len(matrix[i].txt)):
    #             if matrix[i].txt[k]>matrix[j].txt[k]:
    #                 matrix[i],matrix[j]=matrix[j],matrix[i]
    #                 break
    #             elif matrix[i].txt[k]==matrix[j].txt[k]:
    #                 continue
    #             else:
    #                 break
    matrix.sort()
    result=[]
    for item in matrix:
        result.append(item.suffix)

    return result
def build_suffix_array(text):
  """
  Build suffix array of the string text and
  return a list result of the same length as the text
  such that the value result[i] is the index (0-based)
  in text where the i-th lexicographically smallest
  suffix of text starts.
  """
  result = []
  # Implement this function yourself
  return result


if __name__ == '__main__':
  text = sys.stdin.readline().strip()
  print(" ".join(map(str, f1(text))))
