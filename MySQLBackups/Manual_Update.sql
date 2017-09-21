INSERT INTO progress (progress_as_at, progress_year, progress_month, progress_pecentage, progress_description, status_id, action_id) 
Select
  progress_as_at, progress_year, 3, progress_pecentage, progress_description, status_id, action_id
From
  progress
where progress_year='16/17' and progress_month = 2