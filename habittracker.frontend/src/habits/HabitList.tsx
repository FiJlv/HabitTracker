import React, { FC, ReactElement, useRef, useEffect, useState } from 'react';
import { CreateHabitDto, Client, HabitLookupDto } from '../api/api';
import { FormControl } from 'react-bootstrap'; 

const apiClient = new Client('https://localhost:44391');

async function createHabit(habit: CreateHabitDto) {
    await apiClient.create('1.0', habit);
    console.log('Habit is created.');
}

const HabitList: FC<{}> = (): ReactElement => {
    let textInput = useRef(null);
    const [habits, setHabits] = useState<HabitLookupDto[] | undefined>(undefined);

    async function getHabits() {
        const habitListVm = await apiClient.getAll('1.0');
        setHabits(habitListVm.habits);  
    }

    useEffect(() => {
        setTimeout(getHabits, 500);
    }, []);

    const handleKeyPress = (event: React.KeyboardEvent<HTMLInputElement>) => {
        if (event.key === 'Enter') {
            const habit: CreateHabitDto = {
                title: event.currentTarget.value,
            };
            createHabit(habit);
            event.currentTarget.value = '';
            setTimeout(getHabits, 500);
        }
    };

    return (
        <div>
            Habits
            <div>
                <FormControl ref={textInput} onKeyPress={handleKeyPress} />
            </div>
            <section>
                {habits?.map((habit) => (
                    <div>{habit.title}</div>
                ))}
            </section>
        </div>
    );
};
export default HabitList;