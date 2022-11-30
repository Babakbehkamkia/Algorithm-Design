# python3
import sys


def computing_last_to_first(pattern):
    last_to_first=[]
    # nums=[]
    # num=0
    # for symbol in ["A","C","G","T"]:
        
    #     for item in pattern:
    #         if symbol==item:
    #             num+=1
    #     nums.append(num)
    nums={ "A": 0, "C": 0, "G": 0, "T": 0}
    # num=0
    # for symbol in ["A","C","G","T"]:
        
    #     for item in pattern:
    #         if symbol==item:
    #             num+=1
    #     nums.append(num)
    for item in pattern:
        if item!="$":
            nums[item]+=1
    symbols=["A","C","G","T"]
    for index in range(len(symbols)-1):
            nums[symbols[index+1]]+=nums[symbols[index]]
    used=[0 for i in range(len(nums))]
    index=0
    for i in range(len(pattern)):
        p=pattern[i]
        
        if p=="A":
            # result+="A"
            last_to_first.append(1+used[0])
            used[0]+=1
        elif p=="C":
            # result+="C"
            last_to_first.append(1+nums["A"]+used[1])
            used[1]+=1
        elif p=="G":
            # result+="G"
            last_to_first.append(1+nums["C"]+used[2])
            used[2]+=1
        elif p=="T":
            # result+="T"
            last_to_first.append(1+nums["G"]+used[3])
            used[3]+=1
        elif p=="$":
            last_to_first.append(0)
    return last_to_first

def f3(text,patterns):
    result=[]
    last_to_first=computing_last_to_first(text)
    for p in patterns:
        p=p[::-1]
        first=0
        last=len(text)-1
        for i in range(len(p)):
            top_index=first
            bottom_index=last
            count=0
            for j in range(first,last+1):
                if p[i]==text[j] :
                    if count==0:
                        top_index=j
                        count+=1
                        bottom_index=j
                    else:
                        bottom_index=j
            if count==0:
                last=first-1
                break
            first=last_to_first[top_index]
            last=last_to_first[bottom_index]
        result.append(last-first+1)
        # while first<=last:
        #     if len(p)!=0:
        #       l=p[-1]
        #       p=p[:-1]
              # first=
    return result

def PreprocessBWT(bwt):
  """
  Preprocess the Burrows-Wheeler Transform bwt of some text
  and compute as a result:
    * starts - for each character C in bwt, starts[C] is the first position 
        of this character in the sorted array of 
        all characters of the text.
    * occ_count_before - for each character C in bwt and each position P in bwt,
        occ_count_before[C][P] is the number of occurrences of character C in bwt
        from position 0 to position P inclusive.
  """
  # Implement this function yourself
  pass


def CountOccurrences(pattern, bwt, starts, occ_counts_before):
  """
  Compute the number of occurrences of string pattern in the text
  given only Burrows-Wheeler Transform bwt of the text and additional
  information we get from the preprocessing stage - starts and occ_counts_before.
  """
  # Implement this function yourself
  return 0
     


if __name__ == '__main__':
  bwt = sys.stdin.readline().strip()
  pattern_count = int(sys.stdin.readline().strip())
  patterns = sys.stdin.readline().strip().split()
  # Preprocess the BWT once to get starts and occ_count_before.
  # For each pattern, we will then use these precomputed values and
  # spend only O(|pattern|) to find all occurrences of the pattern
  # in the text instead of O(|pattern| + |text|).  
  # starts, occ_counts_before = PreprocessBWT(bwt)
  # occurrence_counts = []
  # for pattern in patterns:
  #   occurrence_counts.append(CountOccurrences(pattern, bwt, starts, occ_counts_before))
  occurrence_counts=f3(bwt,patterns)
  print(' '.join(map(str, occurrence_counts)))
