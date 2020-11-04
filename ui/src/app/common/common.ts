export enum PetType {
  Cat = 0,
  Dog = 1,
  Fish = 2,
  Spider = 3,
  Hamster = 4
}

export function getPetTypes(){
  var result = [];
  for (var petType in PetType) {
    if(isNaN(parseInt(petType, 10))) {
      result.push({key: PetType[petType], value: petType});
    }
  }
  return result;
}

export function getErrorText(errors){
  var result = '';
  for (let error of Object.values(errors)) {
    result += '\n' + error[0];
  }
  return result;
}