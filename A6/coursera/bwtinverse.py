# python3
import sys
def computing_last_to_first(pattern):
    last_to_first=[]
    nums=[]
    num=0
    for symbol in ["A","C","G","T"]:
        
        for item in pattern:
            if symbol==item:
                num+=1
        nums.append(num)
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
            last_to_first.append(1+nums[0]+used[1])
            used[1]+=1
        elif p=="G":
            # result+="G"
            last_to_first.append(1+nums[1]+used[2])
            used[2]+=1
        elif p=="T":
            # result+="T"
            last_to_first.append(1+nums[2]+used[3])
            used[3]+=1
        elif p=="$":
            last_to_first.append(0)
    return last_to_first



def InverseBWT(pattern):
    result=""
    doller_index=0
    index=doller_index
    last_to_first=computing_last_to_first(pattern)
    for i in range(len(pattern)-1):
        p=pattern[index]
        if p=="$":
            result+="$"
            index=last_to_first[index]
        if p=="A":
            result+="A"
            index=last_to_first[index]
        elif p=="C":
            result+="C"
            index=last_to_first[index]
        elif p=="G":
            result+="G"
            index=last_to_first[index]
        elif p=="T":
            result+="T"
            index=last_to_first[index]
    return result[::-1]+"$"
# def InverseBWT(bwt):
#     # write your code here
#     return ""


if __name__ == '__main__':
    bwt = sys.stdin.readline().strip()
    print(InverseBWT(bwt))