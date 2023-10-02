import { FC, ReactElement, useRef, useEffect, useState } from 'react';
import './HabitList.css'
import { FormControl, Button, Modal, Form } from 'react-bootstrap'; // мешает стилизации
import { CreateHabitDto, UpdateHabitDto, Client, HabitDetailsVm } from '../api/api';

const API_URL = 'https://localhost:44391';
const API_VERSION = '1.0';

const apiClient = new Client(API_URL);

async function createHabit(habit: CreateHabitDto) {
    await apiClient.create(API_VERSION, habit);
    console.log('Habit is created.');
}

async function deleteHabit(id: string) {
    await apiClient.delete(id, API_VERSION);
    console.log('Habit is deleted.');
}

const HabitList: FC<{}> = (): ReactElement => {
    const titleInput = useRef<HTMLInputElement | null>(null);
    const instructionInput = useRef<HTMLInputElement | null>(null);
    const habitDaysInput = useRef<HTMLInputElement | null>(null);
    const [habits, setHabits] = useState<HabitDetailsVm[] | undefined>(undefined);
    const [selectedHabit, setSelectedHabit] = useState<HabitDetailsVm | null>(null);
    const [updateHabit, setUpdateHabit] = useState<UpdateHabitDto | null>(null);
    const [showEditModal, setShowEditModal] = useState(false);

    async function getHabits() {
        const habitListVm = await apiClient.getAll(API_VERSION);
        setHabits(habitListVm.habits);  
    }

    useEffect(() => {
        getHabits();
    }, []);

    const handleCreateHabit = async () => {
        const title = titleInput.current?.value;
        const instruction = instructionInput.current?.value;
        const habitDays = habitDaysInput.current?.value;
    
        if (title) {
          const habit: CreateHabitDto = { title, instruction, habitDays}; 
          await createHabit(habit);
          titleInput.current!.value = '';
          instructionInput.current!.value = '';
          habitDaysInput.current!.value = '';
          await getHabits();
        }
    };

    const handleEdit = (habit: HabitDetailsVm) => {
        const updateDto: UpdateHabitDto = {
            id: habit.id,
            title: habit.title,
            instruction: habit.instruction,
            habitDays: habit.habitDays,
        };
        setUpdateHabit(updateDto);
        setShowEditModal(true);
    };
    
    const handleUpdateHabit = async () => {
      if (updateHabit) {
          try {
              await apiClient.update(API_VERSION, updateHabit);
              console.log('Habit is updated.');
              setShowEditModal(false);
              setUpdateHabit(null);
              await getHabits();
          } catch (error) {
              console.error('Error updating habit:', error);
          }
      }
    };

    const handleDelete = async (id: string | undefined) => {
        if (id) {
            await deleteHabit(id);
            await getHabits();
        }
    };

    async function handleShowDetails(id: string) {
        try {
          const habitDetails = await apiClient.get(id, API_VERSION);
          setSelectedHabit(habitDetails);
        } catch (error) {
          console.error('Error fetching habit details:', error);
        }
    };

    const handleToggleDetails = (id: string) => {
        if (selectedHabit && selectedHabit.id === id) {
          setSelectedHabit(null);
        } else {
          handleShowDetails(id);
        }
    };

    return (
    <div>
  
        <div>
            <h1>Add a new habit</h1>
            <FormControl ref={titleInput} placeholder="Title" />
            <FormControl ref={instructionInput} placeholder="Instructions" />
            <FormControl ref={habitDaysInput} placeholder="Habit days" />
            <Button className="Button primary" onClick={handleCreateHabit}>
                Create Habit
            </Button>
        </div>
        
        <section>
        <h1>List of habits</h1>
            {habits?.map((habit) => (
                <div key={habit.id} className ="border py-2 px-4 rounded flex flex-col items-center mb-2">
                    <h2>{habit.title}</h2>
                    <Button className="Button danger" onClick={() => handleDelete(habit.id)}>Delete</Button>
                    <Button className="Button info" onClick={() => handleToggleDetails(habit.id || '')}> 
                        {selectedHabit && selectedHabit.id === habit.id ? 'Hide Details' : 'Show Details'}
                    </Button>
                    <Button className="Button primary" onClick={() => handleEdit(habit)}>Edit</Button>
                    {selectedHabit && selectedHabit.id === habit.id && (
                        <div> 
                            <p>{selectedHabit.instruction}</p>
                            <p>{selectedHabit.habitDays}</p>
                        </div>
                    )}
                </div>
            ))}
        </section>

        <Modal show={showEditModal} onHide={() => setShowEditModal(false)}>
            <Modal.Title><h2>Edit habit</h2></Modal.Title>
            <Modal.Body>
                <Form>
                    <br /> 
                    <Form.Group controlId="editTitle">
                        <Form.Label>Title</Form.Label>
                        <Form.Control type="text" placeholder="Enter new instructions" value={updateHabit?.title} onChange={(e) => setUpdateHabit({ ...updateHabit, title: e.target.value })} />
                    </Form.Group>
                    <br />  
                    <Form.Group controlId="editInstruction">
                        <Form.Label>Instruction</Form.Label>
                        <Form.Control type="text" placeholder="Enter new instructions" value={updateHabit?.instruction} onChange={(e) => setUpdateHabit({ ...updateHabit, instruction: e.target.value })} />
                    </Form.Group>
                    <br />  
                    <Form.Group controlId="editHabitDays">
                        <Form.Label>Habit Days</Form.Label>
                        <Form.Control type="text" placeholder="Enter new habit days" value={updateHabit?.habitDays} onChange={(e) => setUpdateHabit({ ...updateHabit, habitDays: e.target.value })} />
                    </Form.Group>
                    <br /> 
                </Form>
            </Modal.Body>
            <Modal.Footer>
                <Button className="Button primary" onClick={() => setShowEditModal(false)}>Close</Button>
                <Button className="Button primary" onClick={handleUpdateHabit}>Save Changes</Button>
            </Modal.Footer>
        </Modal>

     </div>
    );
};
export default HabitList;