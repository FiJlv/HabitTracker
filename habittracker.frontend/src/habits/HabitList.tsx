import { FC, ReactElement, useRef, useEffect, useState } from 'react';
import { FormControl, Button, Modal, Form } from 'react-bootstrap'; 
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
            Habits
            <div>
                <FormControl ref={titleInput} placeholder="Title" />
                <FormControl ref={instructionInput} placeholder="Instructions" />
                <FormControl ref={habitDaysInput} placeholder="Habit days" />
                <Button variant="primary" onClick={handleCreateHabit}>
                    Create Habit
                </Button>
            </div>
            <section>
                {habits?.map((habit) => (
                    <div>
                        {habit.title}
                        <Button variant="danger" onClick={() => handleDelete(habit.id)}>Delete</Button>
                        <Button variant="info" onClick={() => handleToggleDetails(habit.id || '')}> 
                        {selectedHabit && selectedHabit.id === habit.id ? 'Hide Details' : 'Show Details'}
                         </Button>
                         <Button variant="primary" onClick={() => handleEdit(habit)}>Edit</Button>
                    </div>
                ))}
            </section>
            {selectedHabit && (
            <div>
                <h3>Habit Details</h3>
                <p>Title: {selectedHabit.title}</p>
                <p>Instruction: {selectedHabit.instruction}</p>
                <p>Habit days: {selectedHabit.habitDays}</p>
                <p>Creation date: {selectedHabit.creationDate ? new Date(selectedHabit.creationDate).toDateString() : 'N/A'}</p>
            </div>
        )}
        <Modal show={showEditModal} onHide={() => setShowEditModal(false)}>
                <Modal.Title>Edit Habit</Modal.Title>
                    <Modal.Body>
                      <Form>
                          <Form.Group controlId="editTitle">
                              <Form.Label>Title</Form.Label>
                              <Form.Control type="text" placeholder="Enter a new title" onChange={(e) => setUpdateHabit({ ...updateHabit, title: e.target.value })} />
                          </Form.Group>
                          <Form.Group controlId="editInstruction">
                              <Form.Label>Instruction</Form.Label>
                              <Form.Control type="text" placeholder="Enter new instructions" onChange={(e) => setUpdateHabit({ ...updateHabit, instruction: e.target.value })} />
                          </Form.Group>
                          <Form.Group controlId="editHabitDays">
                              <Form.Label>Habit Days</Form.Label>
                              <Form.Control type="text" placeholder="Enter new habit days" onChange={(e) => setUpdateHabit({ ...updateHabit, habitDays: e.target.value })} />
                          </Form.Group>
                      </Form>
                  </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={() => setShowEditModal(false)}>Close</Button>
                    <Button variant="primary" onClick={handleUpdateHabit}>Save Changes</Button>
                </Modal.Footer>
            </Modal>
        </div>
    );
};
export default HabitList;