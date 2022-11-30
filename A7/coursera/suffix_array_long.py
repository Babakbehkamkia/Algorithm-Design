# python3
import sys


def initial_order(text):
    count=[0 for i in range(5)]
    order=[0 for i in range(len(text))]
    for i in range(len(text)):
        if text[i]=="$":
            count[0]+=1
        elif text[i]=="A":
            count[1]+=1
        elif text[i]=="C":
            count[2]+=1
        elif text[i]=="G":
            count[3]+=1
        elif text[i]=="T":
            count[4]+=1
    for i in range(1,len(count)):
        count[i]+=count[i-1]
    for i in range(len(text)-1,-1,-1):
        c=0
        if text[i]=="A":
            c=1
        elif text[i]=="C":
            c=2
        elif text[i]=="G":
            c=3
        elif text[i]=="T":
            c=4
        count[c]-=1
        order[count[c]]=i
    return order

def initial_class(text,order):
    kelas=[0 for i in range(len(order))]
    kelas[order[0]]=0
    for i in range(1,len(order)):
        if text[order[i]]==text[order[i-1]]:
            kelas[order[i]]=kelas[order[i-1]]
        else:
            kelas[order[i]]=kelas[order[i-1]]+1
    return kelas


def update_order(text,L,order,kelas):
    count=[0 for i in range(len(text))]
    new_order=[0 for i in range(len(text))]
    for i in range(len(text)):
        count[kelas[i]]+=1
    for i in range(1,len(text)):
        count[i]+=count[i-1]
    for i in range(len(text)-1,-1,-1):
        start=(order[i]-L+len(text))%len(text)
        cl=kelas[start]
        count[cl]-=1
        new_order[count[cl]]=start
    return new_order


def update_class(text,L,order,kelas):
    new_kelas=[0 for i in range(len(kelas))]
    new_kelas[order[0]]=0
    for i in range(1,len(order)):
        cur=order[i]
        prev=order[i-1]
        mid=(order[i]+L)%len(text)
        mid_prev=(order[i-1]+L)%len(text)
        if kelas[cur]!=kelas[prev] or kelas[mid]!=kelas[mid_prev]:
            new_kelas[cur]=new_kelas[prev]+1
        else:
            new_kelas[cur]=new_kelas[prev]
    return new_kelas


def build_suffix_array(text):
  """
  Build suffix array of the string text and
  return a list result of the same length as the text
  such that the value result[i] is the index (0-based)
  in text where the i-th lexicographically smallest
  suffix of text starts.
  """
  order=initial_order(text)
  kelas=initial_class(text,order)
  L=1
  while L<len(text):
      order=update_order(text,L,order,kelas)
      kelas=update_class(text,L,order,kelas)
      L*=2
  return order


if __name__ == '__main__':
  text = sys.stdin.readline().strip()
  print(" ".join(map(str, build_suffix_array(text))))
